using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Middlewares;
using TipMeBackend.Models;
using TipMeBackend.Services.UsuarioService;
using TipMeBackend.Services.MesaService;

namespace TipMeBackend.Controllers
{
    [ApiController]
    [Route("api/mozo/")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] Mozo mozoDto)
        {
            Response<string> response = await _usuarioService.RegistrarUsuario(mozoDto);

            if (response.StatusCode == 200) return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }
    }

}