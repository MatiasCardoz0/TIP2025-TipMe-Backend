using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Services.NotaService
{
    public interface INotaService
    {
        Task<Response<List<NotaMesa>>> ObtenerNotasDeMesa(int idMesa);
        Task<Response<string>> GrabarNotaMesa(NotaMesaPostDTO nota);
        Task<Response<string>> ActualizarNotaMesa(NotaMesaPutDTO notaDTO);
        Task<Response<string>> BorrarNotaMesa(int idMesa, int renglonMesa);
        Task<Response<string>> BorrarTodasLasNotas(int idMesa);
    }
}
