using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Phish.Desktop.Wpf.Services;
using Phish.ViewModels;
using DelegateCommand = Prism.Commands.DelegateCommand;


namespace Phish.Desktop.Wpf.ViewModels
{
    public class RandomSetlistViewModel : BusyAwareViewModelBase
    {
        private readonly IWebApiClientService _webApiClientService;
        private readonly IAlertManagerService _alertManagerService;

        public RandomSetlistViewModel(IWebApiClientService webApiClientService, IAlertManagerService alertManagerService)
        {
            _webApiClientService = webApiClientService;
            _alertManagerService = alertManagerService;
        }

        public string PageHeaderText => $"{SetList?.Artist?.Name}, {SetList?.LongDate}";

        public BitmapImage HeaderImageSource => Application.Current.FindResource("SetListImageSourceLarge") as BitmapImage;

        private SetListViewModel _setList;
        public SetListViewModel SetList
        {
            get => _setList;
            set { _setList = value; RaisePropertyChanged(); }
        }

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(LoadedCommandExecute, () => true));

        protected async void LoadedCommandExecute()
        {
            await LoadAsync();
        }

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(RefreshCommandExecute, () => true));

        protected async void RefreshCommandExecute()
        {
            _isLoaded = false;
            await LoadAsync();
        }

        private bool _isLoading;
        private bool _isLoaded;
        private async Task<bool> LoadAsync()
        {
            if (_isLoading || _isLoaded)
            {
                return false;
            }

            _isLoading = true;
            IsBusy = true;
            IsBusyText = "Loading Random SetList...";
            await Task.Run(function: async () =>
            {
                var randomSetlist = await _webApiClientService.GetRandomSetlistAsync();
                return randomSetlist;
            })
                .ContinueWith(task =>
                {
                    try
                    {
                        if (!task.IsFaulted && task.Result != null)
                        {
                            SetList = task.Result;
                            RaisePropertyChanged("PageHeaderText");
                        }
                        else
                        {
                            _alertManagerService.ShowAlert($"Error Occurred Loading Random Setlist", task.Exception.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        _alertManagerService.ShowAlert($"Error Occurred Loading Random Setlist", e.ToString());
                    }
                    finally
                    {
                        IsBusy = false;
                        _isLoading = false;
                        _isLoaded = true;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            return true;
        }
    }
}