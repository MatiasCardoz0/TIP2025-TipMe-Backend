using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Services.UsuarioService
{
    public interface IUsuarioService
    {
        Task<Response<string>> RegistrarUsuario(Mozo userDto);
        Task<Response<AuthDTO>> ObtenerAuthData(string email);
    }
}