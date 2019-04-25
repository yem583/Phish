using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Phish.Desktop.Wpf.Services;
using Phish.Domain;
using Prism.Commands;

namespace Phish.Desktop.Wpf.ViewModels
{
    public class SongHistoryViewModel : BusyAwareViewModelBase
    {
        private readonly IWebApiClientService _webApiClientService;
        private readonly IAlertManagerService _alertManagerService;

        public SongHistoryViewModel(IWebApiClientService webApiClientService,
            IAlertManagerService alertManagerService)
        {
            _webApiClientService = webApiClientService;
            _alertManagerService = alertManagerService;
            Songs = new ObservableCollection<Song>();
            Originals = new ObservableCollection<Song>();
            Covers = new ObservableCollection<Song>();
            Aliases = new ObservableCollection<Song>();
        }

        public string PageHeaderText => "Song History";

        public BitmapImage HeaderImageSource => Application.Current.FindResource("SongHistoryImageSourceLarge") as BitmapImage;

        public ObservableCollection<Song> Songs { get; set; }

        public ObservableCollection<Song> Originals { get; set; }

        public ObservableCollection<Song> Covers { get; set; }

        public ObservableCollection<Song> Aliases { get; set; }

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
            if (_isLoading || _isLoaded) return false;

            _isLoading = true;
            IsBusy = true;
            IsBusyText = "Loading Song History...";
            await Task.Run(async () =>
                {
                    var randomSetlist = await _webApiClientService.GetSongsAsync();
                    return randomSetlist;
                })
                .ContinueWith(task =>
                {
                    try
                    {
                        if (!task.IsFaulted && task.Result != null)
                        {
                            foreach (var song in task.Result.OrderBy(s => s.SongName))
                            {
                                Songs.Add(song);
                                if (song.IsAlias)
                                {
                                    Aliases.Add(song);
                                }

                                if (song.IsCover)
                                {
                                    Covers.Add(song);
                                }

                                if (song.IsOriginal)
                                {
                                    Originals.Add(song);
                                }
                            }
                        }
                        else
                        {
                            _alertManagerService.ShowAlert("Error Occurred Loading Song History", task.Exception.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        _alertManagerService.ShowAlert("Error Occurred Loading Song History", e.ToString());
                    }
                    finally
                    {
                        IsBusy = false;
                        _isLoading = false;
                        _isLoaded = true;
                        task.Dispose();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            Songs.Clear();
            Originals.Clear();
            Covers.Clear();
            Aliases.Clear();
            Songs = null;
            Originals = null;
            Covers = null;
            Aliases = null;
            base.Dispose(disposing);
        }
    }
}