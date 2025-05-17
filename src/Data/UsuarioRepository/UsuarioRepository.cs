using Microsoft.EntityFrameworkCore;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Data.UsuarioRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Context _context;
        public UsuarioRepository(Context context)
        {
            _context = context;
        }

        public async Task<Response<string>> RegistrarUsuario(Mozo user)
        {
            try
            {
                await _context.Mozo.AddAsync(user);
                await _context.SaveChangesAsync();
                return new Response<string>("Usuario registrado correctamente", 200);
            }
            catch (Exception ex)
            {
                return new Response<string>(ex.Message, 500);
            }
        }

        public async Task<Mozo?> ObtenerUsuario(int id)
        {
            return await _context.Mozo.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Response<AuthDTO>> ObtenerAuthData(string email)
        {
            try
            {
                var user = await _context.Mozo.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return new Response<AuthDTO>("Usuario no encontrado", 404);
                }
                return new Response<AuthDTO>(new AuthDTO { Email = user.Email, Password = user.Password }, 200);
            }
            catch (Exception ex)
            {
                return new Response<AuthDTO>(ex.Message, 500);
            }
        }
    }
}