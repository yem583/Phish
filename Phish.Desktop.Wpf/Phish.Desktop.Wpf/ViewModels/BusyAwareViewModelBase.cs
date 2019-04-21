using Telerik.Windows.Controls;

namespace Phish.Desktop.Wpf.ViewModels
{
    public class BusyAwareViewModelBase : ViewModelBase
    {
        private bool _isBusy;
        public virtual bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        private string _isBusyText;
        public virtual string IsBusyText
        {
            get => _isBusyText;
            set
            {
                _isBusyText = value;
                RaisePropertyChanged();
            }
        }

    }
}