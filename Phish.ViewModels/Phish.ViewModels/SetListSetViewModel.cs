using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Phish.ViewModels
{
    public class SetListSetViewModel:ViewModelBase
    {
        public SetListSetViewModel()
        {
            SetListSongs = new ObservableCollection<SetListSongViewModel>();
        }

        private string _setLabel;
        public string SetLabel
        {
            get => _setLabel;
            set
            {
                _setLabel = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<SetListSongViewModel> SetListSongs { get; set; }
    }
}