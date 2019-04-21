using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Phish.Desktop.Wpf.Services
{
    public class AlertManagerService : IAlertManagerService
    {
        private readonly Window _window;

        public RadDesktopAlertManager Manager { get; set; }
        public RadDesktopAlert Alert { get; set; }

        public AlertManagerService(Window window)
        {
            _window = window;
            _window.SizeChanged += MainWindow_SizeChanged;
            _window.LocationChanged += MainWindow_LocationChanged;
        }

        public void ShowNotification(string header, string content)
        {
            if (_window.Dispatcher.CheckAccess())
            {
                var alert = new RadDesktopAlert();
                alert.Header = header;
                alert.Content = content;
                alert.ShowDuration = 3000;
                RadDesktopAlertManager manager = new RadDesktopAlertManager();
                manager.ShowAlert(alert);
            }
            else
            {
                _window.Dispatcher.Invoke(() =>
                {
                    var alert = new RadDesktopAlert();
                    alert.Header = header;
                    alert.Content = content;
                    alert.ShowDuration = 3000;
                    RadDesktopAlertManager manager = new RadDesktopAlertManager();

                    manager.ShowAlert(alert);
                });
            }
        }

        public void ShowAlert(string header, string content)
        {
            if (_window.Dispatcher.CheckAccess())
            {
                RadWindow.Alert(new DialogParameters
                {
                    Content = new TextBox()
                    {
                        TextWrapping = TextWrapping.Wrap,
                        Text = content,
                        MaxHeight = 500,
                        Width = 500
                    },
                    Header = header,
                    Owner = Application.Current.MainWindow

                });
            }
            else
            {
                _window.Dispatcher.Invoke(() =>
                {
                    RadWindow.Alert(new DialogParameters
                    {
                        Content = new TextBox()
                        {
                            TextWrapping = TextWrapping.Wrap,
                            Text = content,
                            MaxHeight = 500,
                            Width = 500
                        },
                        Header = header,
                        Owner = Application.Current.MainWindow

                    });
                });
            }
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            AlertPositioning();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AlertPositioning();
        }

        private void AlertPositioning()
        {
            Manager?.CloseAllAlerts();

            var workingArea = SystemParameters.WorkArea;
            if (_window.WindowState != WindowState.Maximized)
            {
                Manager = new RadDesktopAlertManager(AlertScreenPosition.BottomRight, new Point(
                    -(_window.Left + _window.Width + workingArea.Width - 2 * (_window.Left + _window.Width)),
                    -(_window.Top + _window.Height + workingArea.Height - 2 * (_window.Top + _window.Height))));
            }
            else
            {
                var rect = GetWindowRectangle();

                Manager = new RadDesktopAlertManager(AlertScreenPosition.BottomRight, new Point(rect.Right - workingArea.BottomRight.X, 0));
            }
        }

        private System.Drawing.Rectangle GetWindowRectangle()
        {
            System.Drawing.Rectangle windowRectangle;

            if (_window.WindowState == WindowState.Maximized)
            {
                windowRectangle = System.Windows.Forms.Screen.GetWorkingArea(
                    new System.Drawing.Point((int)_window.Left, (int)_window.Top));
            }
            else
            {
                windowRectangle = new System.Drawing.Rectangle(
                    (int)_window.Left, (int)_window.Top,
                    (int)_window.ActualWidth, (int)_window.ActualHeight);
            }

            return windowRectangle;
        }

    }
}
