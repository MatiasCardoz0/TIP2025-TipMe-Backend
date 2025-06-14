using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Data.UsuarioRepository;
using TipMeBackend.Models;

namespace TipMeBackend.Services.UsuarioService
{
    public class UserService : IUsuarioService
    {
        private readonly IUsuarioRepository _userRepository;

        public UserService(IUsuarioRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<string>> RegistrarUsuario(Mozo mozoDto)
        {
            // User newUser = new Mozo
            // {
            //     Email = authDto.Email,
            //     Password = authDto.Password,
            //     ..
            // };

            return null;
        }

        public async Task<Response<AuthDTO>> ObtenerAuthData(string email)
        {
            return await _userRepository.ObtenerAuthData(email);
        }

        public async Task<Response<string>> ActualizarUsuario(Mozo mozoDto)
        {
            // User newUser = new Mozo
            // {
            //     Email = authDto.Email,
            //     Password = authDto.Password,
            //     ..
            // };

            return null;
        }
    }

}
