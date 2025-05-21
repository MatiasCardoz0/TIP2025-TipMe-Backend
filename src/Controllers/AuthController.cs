using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Services;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Services.UsuarioService;

namespace TipMeBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUsuarioService _usuarioService;

        public AuthController(JwtService jwtService, IUsuarioService usuarioService)
        {
            _jwtService = jwtService;
            _usuarioService = usuarioService;
        }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthDTO dto)
    {
        var response = await _usuarioService.ObtenerAuthData(dto.Email);
        if (response.Data == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        if (response.Data.Password != dto.Password)
            return Unauthorized(new { message = "Credenciales inválidas" });

        // Genera el token JWT
        var token = _jwtService.GenerateToken(response.Data.Email.ToString());
        return Ok(new { token });
    }

        [HttpPost("register")]
        public IActionResult Registrar([FromBody] AuthDTO dto)
        {
            // implementacion  de registro agregando el user a laa db, etc
            return null;
        }
    }

}