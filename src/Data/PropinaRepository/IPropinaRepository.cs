using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Data.PropinaRepository
{
    public interface IPropinaRepository
    {
        Task<Response<List<PropinaDTOGet>>> ObtenerPropinas(int idMozo);
        Task<Response<string>> GrabarPropina(Propina propina);
    }
}
