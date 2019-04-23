﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Phish.Desktop.Wpf.Services;
using Phish.ViewModels;
using Prism.Commands;

namespace Phish.Desktop.Wpf.ViewModels
{
    public class UpcomingShowsViewModel : BusyAwareViewModelBase
    {
        private readonly IWebApiClientService _webApiClientService;
        private readonly IAlertManagerService _alertManagerService;

        public UpcomingShowsViewModel(IWebApiClientService webApiClientService,
            IAlertManagerService alertManagerService)
        {
            _webApiClientService = webApiClientService;
            _alertManagerService = alertManagerService;
            UpcomingShows = new ObservableCollection<ShowViewModel>();
        }

        public string PageHeaderText => "Upcoming Shows";

        public ObservableCollection<ShowViewModel> UpcomingShows { get; set; }

        private DelegateCommand _loadedCommand;

        public DelegateCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(LoadedCommandExecute, () => true));

        protected async void LoadedCommandExecute()
        {
            await LoadAsync();
        }

        private bool _isLoading;

        private async Task<bool> LoadAsync()
        {
            if (_isLoading) return false;

            _isLoading = true;
            IsBusy = true;
            IsBusyText = "Loading Upcoming Shows...";
            await Task.Run(async () =>
                {
                    var randomSetlist = await _webApiClientService.GetUpcomingShowsAsync();
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
                            _alertManagerService.ShowAlert("Error Occurred Loading Upcoming Shows",task.Exception.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        _alertManagerService.ShowAlert("Error Occurred Loading Upcoming Shows", e.ToString());
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