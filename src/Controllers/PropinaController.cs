﻿using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Middlewares;
using TipMeBackend.Models;
using TipMeBackend.Services.PropinaService;

namespace TipMeBackend.Controllers
{
    [ApiController]
    [Route("api/propina/")]
    public class PropinaController : ControllerBase
    {
        private readonly IPropinaService _propinaService;

        public PropinaController(IPropinaService propinaService)
        {
            _propinaService = propinaService;
        }

        [HttpGet("{idMozo}")]
        public async Task<IActionResult> getPropinas(int idMozo)
        {
            Response<List<PropinaDTOGet>> mesas = await _propinaService.ObtenerPropinas(idMozo);

            if (mesas.StatusCode == 200) return Ok(mesas);
            else
            {
                return BadRequest(mesas);
            }
        }

        [HttpPost("grabar")]
        public async Task<IActionResult> grabarPropina([FromBody] PropinaDTO propinaDTO)
        {
            string message = $"Recibiste una nueva propina de {propinaDTO.Monto:C}";
            await WebSocketHandler.SendMessageToMozoAsync(1, message); //mozo 1
            
            var rta = await _propinaService.GrabarPropina(propinaDTO);

            if (rta.StatusCode == 200) {
                return Ok(rta);
                }
            else
            {
                return BadRequest(rta);
            }
        }
    }
}
