using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Services.NotificationService;

namespace TipMeBackend.Controllers

{
    [ApiController]
    [Route("api/notifications/")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("register-push-token")]
        public async Task<IActionResult> RegisterPushToken([FromBody] PushNotificationDTO pushNotifDTO)
        {
            try
            {
                // Guardo el token en la base de datos asociado al mozo
                await _notificationService.RegisterPushToken(pushNotifDTO.IdMozo, pushNotifDTO.ExpoToken);
                return Ok(new { success = true, message = "Push token registrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
