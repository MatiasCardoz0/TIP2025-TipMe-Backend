using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Data.MesaRepository
{
    public interface IMesaRepository
    {
        Task<Response<string>> GrabarMesa(Mesa mesa);
        Task<Response<string>> ActualizarMesa(Mesa mesa, string nombreEstado);
        Task<Response<string>> BorrarMesa(int idMesa);
        Task<Response<List<MesaDTOBase>>> ObtenerMesas(int idMozo);
        Task<Response<(string, int)>> LlamarMozo(int idMesa);
        Task<Response<(string, int)>> PedirCuenta(int idMesa);
    }
}
