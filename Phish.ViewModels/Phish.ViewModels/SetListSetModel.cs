using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Phish.ViewModels
{
    public class SetListSetModel:ViewModelBase
    {
        public SetListSetModel()
        {
            SetListSongs = new ObservableCollection<SetListSongModel>();
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

        public ObservableCollection<SetListSongModel> SetListSongs { get; set; }
    }
}