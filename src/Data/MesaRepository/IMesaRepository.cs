using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Data.MesaRepository
{
    public interface IMesaRepository
    {
        Task<Response<string>> GrabarMesa(Mesa mesa);
        Task<Response<List<MesaDTOGet>>> ObtenerMesas(int idMozo);
        Task<Response<(string, int)>> LlamarMozo(int idMesa);
        Task<Response<(string, int)>> PedirCuenta(int idMesa);
    }
}
