using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Middlewares;
using TipMeBackend.Models;
using TipMeBackend.Services.MesaService;

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
        public async Task<IActionResult> recibirLlamado(int idMesa, int idMozo)
        {
            var rta = await _mesaService.LlamarMozo(idMesa);

            string message = $"Llamado de la mesa {rta.Data.Item2}";
            await WebSocketHandler.SendMessageToMozoAsync(idMozo, message);

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
        public async Task<IActionResult> pedirCuenta(int idMesa, int idMozo)
        {
            var rta = await _mesaService.PedirCuenta(idMesa);

            string message = $"Pedido de cuenta de la mesa {rta.Data.Item2}";
            await WebSocketHandler.SendMessageToMozoAsync(idMozo, message);

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

        [HttpDelete("borrar")]
        public async Task<IActionResult> borrarMesa([FromBody] int id)
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
