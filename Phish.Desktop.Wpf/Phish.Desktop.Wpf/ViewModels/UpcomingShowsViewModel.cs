using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Phish.Desktop.Wpf.Services;
using Phish.Domain;
using Phish.ViewModels;
using Prism.Commands;

namespace Phish.Desktop.Wpf.ViewModels
{
    public class UpcomingShowsViewModel : BusyAwareViewModelBase
    {
    
        public UpcomingShowsViewModel(IWebApiClientService webApiClientService,
            IAlertManagerService alertManagerService)
        :base(webApiClientService,alertManagerService)
        {
            UpcomingShows = new ObservableCollection<UpcomingShow>();
        }

        public string PageHeaderText => "Upcoming Shows";
    
        public BitmapImage HeaderImageSource => Application.Current.FindResource("UpcomingShowsImageSourceLarge") as BitmapImage;

        public ObservableCollection<UpcomingShow> UpcomingShows { get; set; }
        
        protected override async Task<bool> LoadAsync()
        {
            if (IsLoaded || IsBusy)
            {
                return false;
            }
            IsBusy = true;
            IsBusyText = "Loading Upcoming Shows...";
            await Task.Run(async () =>
                {
                    var randomSetlist = await WebApiClientService.GetUpcomingShowsAsync();
                    return randomSetlist;
                })
                .ContinueWith(task =>
                {
                    try
                    {
                        if (!task.IsFaulted && task.Result != null)
                        {
                            foreach (var show in task.Result)
                            {
                                UpcomingShows.Add(show);
                            }
                        }
                        else
                        {
                            AlertManagerService.ShowAlert("Error Occurred Loading Upcoming Shows",task.Exception.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        AlertManagerService.ShowAlert("Error Occurred Loading Upcoming Shows", e.ToString());
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