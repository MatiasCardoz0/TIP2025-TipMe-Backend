using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Data.PropinaRepository;
using TipMeBackend.Models;

namespace TipMeBackend.Services.PropinaService
{
    public class PropinaService : IPropinaService
    {
        private readonly IPropinaRepository _propinaRepository;
        public PropinaService(IPropinaRepository propinaRepository)
        {
            _propinaRepository = propinaRepository;
        }
        public async Task<Response<string>> GrabarPropina(PropinaDTO propinaDTO)
        {
            Propina nuevaPropina = new Propina 
            {
                Monto = propinaDTO.Monto,
                Fecha = propinaDTO.Fecha,
                IdMesa = propinaDTO.IdMesa,
                IdMozo = propinaDTO.IdMozo
            };

            return await _propinaRepository.GrabarPropina(nuevaPropina);
        }

        public async Task<Response<List<PropinaDTOGet>>> ObtenerPropinas(int idMozo)
        {
            return await _propinaRepository.ObtenerPropinas(idMozo);
        }
    }
}
