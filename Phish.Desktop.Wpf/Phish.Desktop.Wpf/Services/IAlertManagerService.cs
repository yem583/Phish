namespace Phish.Desktop.Wpf.Services
{
    public interface IAlertManagerService
    {
        void ShowAlert(string header, string content);

        void ShowNotification(string header, string content);
    }
}