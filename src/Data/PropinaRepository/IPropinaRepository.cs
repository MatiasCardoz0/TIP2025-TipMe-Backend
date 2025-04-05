using TipMeBackend.Models;

namespace TipMeBackend.Data.PropinaRepository
{
    public interface IPropinaRepository
    {
        Task<Response<List<Propina>>> ObtenerPropinas(int idMozo);
        Task<Response<string>> GrabarPropina(Propina propina);
    }
}
