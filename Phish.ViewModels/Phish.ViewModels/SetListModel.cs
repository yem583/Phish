using System.Collections.ObjectModel;
using Phish.Domain;

namespace Phish.ViewModels
{
    public class SetListModel : ViewModelBase
    {
        public SetListModel()
        {
            Sets = new ObservableCollection<SetListSetModel>();
            FooterItems = new ObservableCollection<SetListFooterItemModel>();
        }

        #region Properties

        public ObservableCollection<SetListSetModel> Sets { get; set; }

        public ObservableCollection<SetListFooterItemModel> FooterItems { get; set; }

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

        private string _shortDate;
        public string ShortDate
        {
            get => _shortDate;
            set
            {
                _shortDate = value;
                NotifyPropertyChanged();
            }
        }

        private string _longDate;
        public string LongDate
        {
            get => _longDate;
            set
            {
                _longDate = value;
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

        private string _gapChart;
        public string GapChart
        {
            get => _gapChart;
            set
            {
                _gapChart = value;
                NotifyPropertyChanged();
            }
        }

        private decimal? _rating;
        public decimal? Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                NotifyPropertyChanged();
            }
        }

        private string _relativeDate;
        public string RelativeDate
        {
            get => _relativeDate;
            set
            {
                _relativeDate = value;
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

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}