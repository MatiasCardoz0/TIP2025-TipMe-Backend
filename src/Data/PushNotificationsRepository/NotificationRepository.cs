using Microsoft.EntityFrameworkCore;
using TipMeBackend.Data.PushNotificationsRepository;
using TipMeBackend.Models;

namespace TipMeBackend.Data.NotificationRepository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly Context _context;

        public NotificationRepository(Context context)
        {
            _context = context;
        }

        public async Task RegisterPushToken(int idMozo, string token)
        {
            var waiter = await _context.Mozo.FirstOrDefaultAsync(w => w.Mozo_Id == idMozo);
            if (waiter == null)
            {
                throw new Exception("El mozo con id " + idMozo + " no existe");
            }

            waiter.PushToken = token;
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetPushToken(int idMozo)
        {
            var waiter = await _context.Mozo.FirstOrDefaultAsync(w => w.Mozo_Id == idMozo);
            if (waiter == null || string.IsNullOrEmpty(waiter.PushToken))
            {
                throw new Exception("No se encontró el token de notificación para el mozo");
            }

            return waiter.PushToken;
        }
    }
}