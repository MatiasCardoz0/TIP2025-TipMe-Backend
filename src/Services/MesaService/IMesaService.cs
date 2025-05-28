using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Services.MesaService
{
    public interface IMesaService
    {
        Task<Response<List<MesaDTOGet>>> ObtenerMesas(int idMozo);
        Task<Response<string>> GrabarMesa(MesaDTO mesaDto);
        Task<Response<(string, int)>> LlamarMozo(int idMesa);
        Task<Response<(string, int)>> PedirCuenta(int idMesa);
    }
}
