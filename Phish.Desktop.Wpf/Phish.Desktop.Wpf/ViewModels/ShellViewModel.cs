using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Phish.Desktop.Wpf.Events;
using Phish.Desktop.Wpf.Services;
using Phish.Desktop.Wpf.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace Phish.Desktop.Wpf.ViewModels
{
    public class ShellViewModel : BusyAwareViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        public string Title => "Phish Facts";

        public ShellViewModel(IEventAggregator eventAggregator, IRegionManager regionManager,
            IWebApiClientService apiClientService,IAlertManagerService alertManagerService)
        :base(apiClientService,alertManagerService)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
        }

        private DelegateCommand<string> _showViewCommand;
        public DelegateCommand<string> ShowViewCommand => _showViewCommand ?? (_showViewCommand = new DelegateCommand<string>(ShowViewCommandExecute, (e) => true));

        protected virtual void ShowViewCommandExecute(string viewParameter)
        {
            ShowView(viewParameter);
        }

        protected void ShowView(string viewIdentifier)
        {
            var showViewEvent = _eventAggregator.GetEvent<ShowViewEvent>();
            var showViewEventArgs = new ShowViewEventArgs();
            showViewEventArgs.Id = viewIdentifier;
            showViewEventArgs.ViewIdentifier = viewIdentifier;
            showViewEvent.Publish(showViewEventArgs);
        }

        private DelegateCommand<StateChangeEventArgs> _closeCommand;
        public DelegateCommand<StateChangeEventArgs> CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand<StateChangeEventArgs>(CloseCommandExecute, (a) => true));

        private void CloseCommandExecute(StateChangeEventArgs e)
        {
            for (var index = 0; index < e.Panes.ToList().Count; index++)
            {
                var pane = e.Panes.ToList()[index] as RadDocumentPane;
                if (pane.Content is MyViewBase @base)
                {
                    if (@base.DataContext is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                    (@base.ViewContainer as IDisposable).Dispose();
                }
                
                pane.DataContext = null;
                if (pane.Content is ContentControl control)
                {
                    control.Content = null;
                }

                _regionManager.Regions["MainContentRegion"].Remove(pane);
                pane.RemoveFromParent();
                pane = null;
            }
        }
    }
}
