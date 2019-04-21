using Phish.Domain;

namespace Phish.ViewModels
{
    public class ShowViewModel : ViewModelBase
    {
        private int? _tourId;
        public int? TourId
        {
            get => _tourId;
            set
            {
                _tourId = value;
                NotifyPropertyChanged();
            }
        }

        private string _setListNotes;
        public string SetListNotes
        {
            get => _setListNotes;
            set
            {
                _setListNotes = value;
                NotifyPropertyChanged();
            }
        }

        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                _tourName = value;
                NotifyPropertyChanged();
            }
        }

        private string _tourWhen;
        public string TourWhen
        {
            get => _tourWhen;
            set
            {
                _tourWhen = value;
                NotifyPropertyChanged();
            }
        }

        private int? _showId;
        public int? ShowId
        {
            get => _showId;
            set
            {
                _showId = value;
                NotifyPropertyChanged();
            }
        }

        private string _showDate;
        public string ShowDate
        {
            get => _showDate;
            set
            {
                _showDate = value;
                NotifyPropertyChanged();
            }
        }

        private string _billedAs;
        public string BilledAs
        {
            get => _billedAs;
            set
            {
                _billedAs = value;
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

        private Artist _artist;
        public Artist Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                NotifyPropertyChanged();
            }
        }

        private Venue _venue;
        public Venue Venue
        {
            get => _venue;
            set
            {
                _venue = value;
                NotifyPropertyChanged();
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                NotifyPropertyChanged();
            }
        }

       
    }
}