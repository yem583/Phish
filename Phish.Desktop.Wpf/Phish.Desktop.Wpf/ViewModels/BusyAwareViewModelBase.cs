using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Phish.Desktop.Wpf.Services;
using Telerik.Windows.Controls;
using DelegateCommand = Prism.Commands.DelegateCommand;


namespace Phish.Desktop.Wpf.ViewModels
{
    public class BusyAwareViewModelBase : ViewModelBase
    {
   
        public BusyAwareViewModelBase(IWebApiClientService webApiClientService,
            IAlertManagerService alertManagerService)
        {
            WebApiClientService = webApiClientService;
            AlertManagerService = alertManagerService;
        }

        #region Properties

        public IWebApiClientService WebApiClientService { get; }

        public IAlertManagerService AlertManagerService { get; }

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


        private bool _isLoaded;
        public virtual bool IsLoaded
        {
            get => _isLoaded;
            set
            {
                _isLoaded = value;
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

        #endregion

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(RefreshCommandExecute, () => true));

        protected async void RefreshCommandExecute()
        {
            IsLoaded = false;
            await LoadAsync();
        }

        private DelegateCommand _loadedCommand;

        public DelegateCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(LoadedCommandExecute, () => true));

        protected async void LoadedCommandExecute()
        {
            await LoadAsync();
        }

        protected virtual async Task<bool> LoadAsync()
        {
            return true;
        }

    }
}