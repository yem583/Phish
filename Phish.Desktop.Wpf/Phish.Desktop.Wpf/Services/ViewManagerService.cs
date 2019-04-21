using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Phish.Desktop.Wpf.Events;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using Telerik.Windows.Controls;
using Unity;

namespace Phish.Desktop.Wpf.Services
{
    public class ViewManagerService : IViewManagerService
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _container;

        public ViewManagerService(IRegionManager regionManager, IEventAggregator eventAggregator,
            IContainerRegistry containerRegistry)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _container = containerRegistry.GetContainer();

            var showViewEvent = eventAggregator.GetEvent<ShowViewEvent>();
            showViewEvent.Subscribe(ShowViewEventHandler, ThreadOption.UIThread, true);

            var closeViewEvent = eventAggregator.GetEvent<CloseViewEvent>();
            closeViewEvent.Subscribe(CloseViewEventHandler, ThreadOption.UIThread, false);
        }

        private void CloseViewEventHandler(ShowViewEventArgs closeView)
        {
            var mainRegion = _regionManager.Regions[Regions.MainContentRegion];
            var viewExists = mainRegion.Views.FirstOrDefault(p => ((RadDocumentPane)p).Tag.ToString() == closeView.Id);
            if (viewExists != null)
            {
                mainRegion.Remove(viewExists);
            }
        }

        private void ShowViewEventHandler(ShowViewEventArgs showView)
        {
            var mainRegion = _regionManager.Regions[Regions.MainContentRegion];
            var viewExists = mainRegion.Views.FirstOrDefault(p => ((RadDocumentPane)p).Tag.ToString() == showView.Id);

            RadDocumentPane radDocumentPane = null;
            if (viewExists == null)
            {
                var view = _container.Resolve(GetViewClassType(showView.ViewIdentifier));
                radDocumentPane = new RadDocumentPane();
                radDocumentPane.Content = view;
                radDocumentPane.Tag = showView.Id;
                radDocumentPane.HeaderTemplate = Application.Current.FindResource("RadDocumentPaneHeaderTemplate") as DataTemplate;
                var myBinding = new Binding();
                myBinding.Source = (radDocumentPane.Content as FrameworkElement).DataContext;
                BindingOperations.SetBinding(radDocumentPane, HeaderedContentControl.HeaderProperty, myBinding);
                mainRegion.Add(radDocumentPane);
            }
            else
            {
                radDocumentPane = viewExists as RadDocumentPane;
            }
            radDocumentPane.IsActive = true;
            radDocumentPane.IsHidden = false;
            mainRegion.Activate(radDocumentPane);

        }

        public static Type GetViewClassType(string viewIdentifier)
        {
            return Type.GetType($"Phish.Desktop.Wpf.Views.{viewIdentifier}");
        }
    }
}