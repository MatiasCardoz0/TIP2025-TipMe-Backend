using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;
using TipMeBackend.Services;
using TipMeBackend.Services.NotificationService;
using TipMeBackend.Services.NotificationService.NotificationService;
using TipMeBackend.Services.PropinaService;

namespace TipMeBackend.Controllers
{
    [ApiController]
    [Route("api/propina/")]
    public class PropinaController : ControllerBase
    {
        private readonly IPropinaService _propinaService;
        private readonly INotificationService _notificationService;

        public PropinaController(IPropinaService propinaService, INotificationService notificationService)
        {
            _propinaService = propinaService;
            _notificationService = notificationService;
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
            var rta = await _propinaService.GrabarPropina(propinaDTO);

            if (rta.StatusCode == 200){
                await _notificationService.SendPushNotification(
                    propinaDTO.IdMozo,
                    "Nueva Propina",
                    $"Recibiste una propina de ${propinaDTO.Monto}!"
                );

                return Ok(rta); 
            }
            else
            {
                return BadRequest(rta);
            }
        }
    }
}