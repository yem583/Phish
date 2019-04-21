using System;
using System.Threading.Tasks;
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

        private SetListModel _setList;
        public SetListModel SetList
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

        private bool _isLoading;

        private async Task<bool> LoadAsync()
        {
            if (_isLoading)
            {
                return false;
            }

            _isLoading = true;
            IsBusy = true;
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
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            return true;
        }
    }
}