namespace Phish.ViewModels
{
    public class SetListFooterItemModel : ViewModelBase
    {
        private string _title;
        private string _value;

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                NotifyPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }
    }
}