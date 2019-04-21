using System;
using System.Collections.Generic;
using System.Text;
using Phish.Desktop.Wpf.Events;
using Prism.Commands;
using Prism.Events;
using Telerik.Windows.Controls;

namespace Phish.Desktop.Wpf.ViewModels
{
    public class ShellViewModel : BusyAwareViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        public string Title => "Phish Facts";

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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
    }
}
