namespace Phish.ViewModels
{
    public class SetListSongModel: ViewModelBase
    {
        private string _supTitle;
        public string SupTitle
        {
            get => _supTitle;
            set
            {
                _supTitle = value;
                NotifyPropertyChanged();
            }
        }

        private string _supValue;
        public string SupValue
        {
            get => _supValue;
            set
            {
                _supValue = value;
                NotifyPropertyChanged();
            }
        }

        private string _song;
        public string Song
        {
            get => _song;
            set
            {
                _song = value;
                NotifyPropertyChanged();
            }
        }
        
        private string _link;
        public string Link
        {
            get => _link;
            set
            {
                _link = value;
                NotifyPropertyChanged();
            }
        }

        private bool _rightIntoNextSong;
        public bool RightIntoNextSong
        {
            get => _rightIntoNextSong;
            set
            {
                _rightIntoNextSong = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSetCloser;
        public bool IsSetCloser
        {
            get => _isSetCloser;
            set
            {
                _isSetCloser = value;
                NotifyPropertyChanged();
            }
        }
    }
}