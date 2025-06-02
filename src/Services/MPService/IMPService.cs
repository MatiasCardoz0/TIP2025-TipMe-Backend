using MercadoPago.Resource.Preference;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Services.MPService
{
    public interface IMPService
    {
        Task<Response<string>> GetPreferenceId(PropinaDTO propinaDTO);
    }
}
