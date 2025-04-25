namespace TipMeBackend.Services.NotificationService
{
    public interface INotificationService
    {
        Task RegisterPushToken(int idMozo, string token);
        Task SendPushNotification(int idMozo, string title, string body);
    }
}