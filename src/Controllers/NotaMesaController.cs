using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Services.NotaService;

namespace TipMeBackend.Controllers
{
    [ApiController]
    [Route("api/nota/")]
    public class NotaMesaController : ControllerBase
    {
        private readonly INotaService _notaService;

        public NotaMesaController(INotaService notaService)
        {
            _notaService = notaService;
        }

        [HttpGet()]
        public async Task<IActionResult> getNotasDeMesa(int idMesa)
        {
            var notas = await _notaService.ObtenerNotasDeMesa(idMesa);

            if (notas.StatusCode == 200) return Ok(notas);
            else
            {
                return BadRequest(notas);
            }
        }

        [HttpPost("grabar")]
        public async Task<IActionResult> grabarNotaEnMesa([FromBody] NotaMesaPostDTO notaDTO)
        {
            var rta = await _notaService.GrabarNotaMesa(notaDTO);

            if (rta.StatusCode == 200)
            {
                return Ok(rta);
            }
            else
            {
                return BadRequest(rta);
            }
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> actualizarNotaEnMesa([FromBody] NotaMesaPutDTO notaDTO)
        {
            var rta = await _notaService.ActualizarNotaMesa(notaDTO);

            if (rta.StatusCode == 200)
            {
                return Ok(rta);
            }
            else
            {
                return BadRequest(rta);
            }
        }

        [HttpDelete("borrarUnaNota/{mesaId}/{renglon}")]
        public async Task<IActionResult> borrarUnaNotaDeMesa(int mesaId, int renglon)
        {
            var rta = await _notaService.BorrarNotaMesa(mesaId, renglon);

            if (rta.StatusCode == 200)
            {
                return Ok(rta);
            }
            else
            {
                return BadRequest(rta);
            }
        }

        [HttpDelete("borrarNotas/{idMesa}")]
        public async Task<IActionResult> borrarUnaNotaDeMesa(int idMesa)
        {
            var rta = await _notaService.BorrarTodasLasNotas(idMesa);

            if (rta.StatusCode == 200)
            {
                return Ok(rta);
            }
            else
            {
                return BadRequest(rta);
            }
        }
    }
}
