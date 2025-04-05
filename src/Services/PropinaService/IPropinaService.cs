using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Services.PropinaService
{
    public interface IPropinaService
    {
        Task<Response<List<Propina>>> ObtenerPropinas(int idMozo);
        Task<Response<string>> GrabarPropina(PropinaDTO propinaDTO);
    }
}
