using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Middlewares;
using TipMeBackend.Models;
using TipMeBackend.Services.MesaService;
using System.Text.Json;

namespace TipMeBackend.Controllers
{
    [ApiController]
    [Route("api/mesa/")]
    public class MesaController : ControllerBase
    {
        private readonly IMesaService _mesaService;

        public MesaController(IMesaService mesaService)
        {
            _mesaService = mesaService;

        }
        [HttpGet("historico/{idMozo}")]
        public async Task<IActionResult> getMesas(int idMozo)
        {
            Response<List<MesaDTOBase>> mesas = await _mesaService.ObtenerMesas(idMozo);

            if (mesas.StatusCode == 200) return Ok(mesas);
            else
            {
                return BadRequest(mesas);
            }
        }

        [HttpPost("grabar")]
        public async Task<IActionResult> grabarMesa([FromBody] MesaDTO mesaDto)
        {
            var rta = await _mesaService.GrabarMesa(mesaDto);

            if (rta.StatusCode == 200) return Ok(rta);
            else
            {
                return BadRequest(rta);
            }
        }

        //endpoint para recibir el llamado del mozo desde el cliente
        [HttpPost("llamarMozo")]
        public async Task<IActionResult> recibirLlamado(int idMesa, [FromBody] LlamadoMozoDTO body)
        {

                var rta = await _mesaService.LlamarMozo(idMesa);

                string message = $"Llamado de la mesa {rta.Data.Item2}";
                if (!string.IsNullOrWhiteSpace(body?.Nota))
                {
                    message += $": {body.Nota}";
                }
                var notaPayload = new { nota = body.Nota, mesa = rta.Data.Item2 };
                await WebSocketHandler.SendMessageToMozoAsync(rta.Data.Item3, message);

            if (rta.StatusCode == 200)
            {
                return Ok(rta);
            }
            else
            {
                return BadRequest(rta);
            }
        }

        [HttpPost("pedirCuenta")]
        public async Task<IActionResult> pedirCuenta(int idMesa)
        {
            var rta = await _mesaService.PedirCuenta(idMesa);

            string message = $"Pedido de cuenta de la mesa {rta.Data.Item2}";
            await WebSocketHandler.SendMessageToMozoAsync(rta.Data.Item3, message);

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
        public async Task<IActionResult> actualizarMesa([FromBody] MesaDTOBase mesa)
        {
            var rta = await _mesaService.ActualizarMesa(mesa);

            if (rta.StatusCode == 200) return Ok(rta);
            else
            {
                return BadRequest(rta);
            }
        }

        [HttpDelete("borrar/{id}")]
        public async Task<IActionResult> borrarMesa(int id)
        {
            var rta = await _mesaService.BorrarMesa(id);

            if (rta.StatusCode == 200) return Ok(rta);
            else
            {
                return BadRequest(rta);
            }
        }
    }
}
