using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Services.MPService;

namespace TipMeBackend.Controllers
{
    [ApiController]
    [Route("api/mp/")]
    public class MPController : ControllerBase
    {
        private readonly IMPService _mpService;

        public MPController(IMPService mpService)
        {
            _mpService = mpService;
        }

        [HttpGet("preferenceId/{monto}")]
        public async Task<IActionResult> getPreferenceId(decimal monto)
        {          
            var rta = await _mpService.GetPreferenceId(monto);

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
