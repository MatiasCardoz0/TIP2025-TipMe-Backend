using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Data.NotaRepository;
using TipMeBackend.Models;

namespace TipMeBackend.Services.NotaService
{
    public class NotaService : INotaService
    {
        private readonly INotaRepository _notaRepository;
        public NotaService(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        public Task<Response<string>> ActualizarNotaMesa(NotaMesaPutDTO notaDTO)
        {
            return _notaRepository.ActualizarNotaMesa(notaDTO);
        }

        public Task<Response<string>> BorrarNotaMesa(int idMesa, int renglonMesa)
        {
            return _notaRepository.BorrarNotaMesa(idMesa, renglonMesa);
        }

        public Task<Response<string>> BorrarTodasLasNotas(int idMesa)
        {
            return _notaRepository.BorrarTodasLasNotas(idMesa);
        }

        public Task<Response<string>> GrabarNotaMesa(NotaMesaPostDTO nota)
        {
            return _notaRepository.GrabarNotaMesa(nota);
        }

        public Task<Response<List<NotaMesa>>> ObtenerNotasDeMesa(int idMesa)
        {
            return _notaRepository.ObtenerNotasDeMesa(idMesa);
        }
    }
}
