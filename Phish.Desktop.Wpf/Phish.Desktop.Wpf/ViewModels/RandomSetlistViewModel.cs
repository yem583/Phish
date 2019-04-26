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
 
        public RandomSetlistViewModel(IWebApiClientService webApiClientService, IAlertManagerService alertManagerService)
        :base(webApiClientService,alertManagerService)
        {

        }

        public string PageHeaderText => $"{SetList?.Artist?.Name}, {SetList?.LongDate}";

        public BitmapImage HeaderImageSource => Application.Current.FindResource("SetListImageSourceLarge") as BitmapImage;

        private SetListViewModel _setList;
        public SetListViewModel SetList
        {
            get => _setList;
            set { _setList = value; RaisePropertyChanged(); }
        }
        
        protected override async Task<bool> LoadAsync()
        {
            if (IsLoaded || IsBusy)
            {
                return false;
            }

            IsBusy = true;
            IsBusyText = "Loading Random SetList...";
            await Task.Run(function: async () =>
            {
                var randomSetlist = await WebApiClientService.GetRandomSetlistAsync();
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
                            AlertManagerService.ShowAlert($"Error Occurred Loading Random Setlist", task.Exception.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        AlertManagerService.ShowAlert($"Error Occurred Loading Random Setlist", e.ToString());
                    }
                    finally
                    {
                        IsBusy = false;
                        IsLoaded = true;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            return true;
        }
    }
}