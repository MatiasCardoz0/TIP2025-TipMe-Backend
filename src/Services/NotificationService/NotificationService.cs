using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TipMeBackend.Data.NotificationRepository;
using TipMeBackend.Data.PushNotificationsRepository;


namespace TipMeBackend.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly HttpClient _httpClient;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
            _httpClient = new HttpClient();
        }

        public async Task RegisterPushToken(int idMozo, string token)
        {
            await _notificationRepository.RegisterPushToken(idMozo, token);
        }

        public async Task SendPushNotification(int idMozo, string title, string body)
        {
            var token = await _notificationRepository.GetPushToken(idMozo);

            var notification = new
            {
                to = token,
                sound = "default",
                title,
                body,
                data = new { message = body }
            };

            var json = JsonConvert.SerializeObject(notification);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("https://exp.host/--/api/v2/push/send", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error enviando notificación: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error enviando notificación: {ex.Message}");
            }
        }
    }
}