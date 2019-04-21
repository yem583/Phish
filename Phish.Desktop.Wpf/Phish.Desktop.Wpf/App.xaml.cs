using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Phish.Desktop.Wpf.Docking;
using Phish.Desktop.Wpf.Services;
using Phish.Desktop.Wpf.Views;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using Telerik.Windows.Controls;

namespace Phish.Desktop.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(typeof(IContainerRegistry), containerRegistry);
            containerRegistry.RegisterSingleton<IAlertManagerService, AlertManagerService>();
            containerRegistry.RegisterSingleton<IViewManagerService, ViewManagerService>();
            containerRegistry.RegisterSingleton<IWebApiClientService, WebApiClientService>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            regionAdapterMappings.RegisterMapping(typeof(RadDocking), Container.Resolve<DockingRegionAdapter>());
        }

        public static bool IsShuttingDown()
        {
            try
            {
                Application.Current.ShutdownMode = Application.Current.ShutdownMode;
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        protected override Window CreateShell()
        {
            if (IsShuttingDown())
            {
                return Current.MainWindow;
            }
            Container.Resolve<IViewManagerService>();//force creation of ViewManager instance so it will subscribe to ShowViewEvent
            var mainWindow = Container.Resolve<ShellView>();
            Current.Exit += Current_Exit;
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Current.MainWindow = mainWindow;
            return mainWindow;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //_logger.LogError("Unhandled Error Occurred -{0}", e.Exception);
            var alertManagerService = Container.Resolve<IAlertManagerService>();
            alertManagerService.ShowAlert("Unhandled Error Occurred, please contact support", e.Exception.ToString());
            e.Handled = true;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            //_logger.LogInformation($"User Exited App -{_userContext?.CurrentUser?.UserName}");
        }
    }
}
