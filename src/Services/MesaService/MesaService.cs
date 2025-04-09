using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Data.MesaRepository;
using TipMeBackend.Models;

namespace TipMeBackend.Services.MesaService
{
    public class MesaService : IMesaService
    {
        private readonly IMesaRepository _mesaRepository;

        public MesaService(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;

        }

        public async Task<Response<string>> GrabarMesa(MesaDTO mesaDto)
        {           
            Mesa nuevaMesa = new Mesa
            {
                Nombre = mesaDto.Nombre,
                Numero = mesaDto.Numero,
                MozoId = mesaDto.MozoId,
                QR = mesaDto.QR,
                Estado = mesaDto.Estado
            };

            if (String.IsNullOrEmpty(nuevaMesa.Nombre)) 
            {
                return new Response<string>("El nombre no puede ser vacio.", 400);
            }

            if (nuevaMesa.Nombre.Length > 60)
            {
                return new Response<string>("El nombre no puede contener mas de 60 caracteres.", 400);
            }

            if (nuevaMesa.Numero < 0)
            {
                return new Response<string>("El numero de mesa debe ser mayos a 0.", 400);
            }

            if (String.IsNullOrEmpty(nuevaMesa.QR))
            {
                return new Response<string>("El QR no puede ser vacio.", 400);
            }

            return await _mesaRepository.GrabarMesa(nuevaMesa);
            
        }

        public async Task<Response<List<MesaDTOGet>>> ObtenerMesas(int idMozo)
        {
            return await _mesaRepository.ObtenerMesas(idMozo);
        }
    }
}
