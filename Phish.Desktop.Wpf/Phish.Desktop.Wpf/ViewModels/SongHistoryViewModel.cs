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
   
        public SongHistoryViewModel(IWebApiClientService webApiClientService,
            IAlertManagerService alertManagerService)
        :base(webApiClientService,alertManagerService)
        {
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

        protected override async Task<bool> LoadAsync()
        {
            if (IsLoaded || IsBusy)
            {
                return false;
            }
            IsBusy = true;
            IsBusyText = "Loading Song History...";
            await Task.Run(async () =>
                {
                    var randomSetlist = await WebApiClientService.GetSongsAsync();
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
                            AlertManagerService.ShowAlert("Error Occurred Loading Song History", task.Exception.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        AlertManagerService.ShowAlert("Error Occurred Loading Song History", e.ToString());
                    }
                    finally
                    {
                        IsBusy = false;
                        IsLoaded = true;
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