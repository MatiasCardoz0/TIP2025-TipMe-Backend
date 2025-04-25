namespace TipMeBackend.Data.PushNotificationsRepository
{
    public interface INotificationRepository
    {
        Task RegisterPushToken(int idMozo, string token);
        Task<string> GetPushToken(int idMozo);
    }
}
