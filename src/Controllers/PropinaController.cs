using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Controllers.DTOs;
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

        [HttpGet(":idMozo")]
        public async Task<IActionResult> getPropinas(int idMozo)
        {
            Response<List<Propina>> mesas = await _propinaService.ObtenerPropinas(idMozo);

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

            if (rta.StatusCode == 200) return Ok(rta);
            else
            {
                return BadRequest(rta);
            }
        }
    }
}
