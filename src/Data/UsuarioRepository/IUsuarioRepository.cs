using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Data.UsuarioRepository
{
    public interface IUsuarioRepository
    {
        Task<Response<string>> RegistrarUsuario(Mozo newUser);
        Task<Mozo?> ObtenerUsuario(int id);
        Task<Response<AuthDTO>> ObtenerAuthData(string email);
    }
}