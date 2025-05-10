using Microsoft.EntityFrameworkCore;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Data.MesaRepository
{
    public class MesaRepository : IMesaRepository
    {
        private readonly Context _context;

        public MesaRepository(Context context)
        {
            _context = context;
        }

        public async Task<Response<string>> GrabarMesa(Mesa mesa)
        {
            await _context.Mesa.AddAsync(mesa);
            int respuesta = await _context.SaveChangesAsync();

            return new Response<string>(respuesta > 0 ? "Registro realizado con éxito" : "Ha ocurrido un error al realizar el registro.", respuesta > 0 ? 200 : 400);
        }

        public async Task<Response<List<MesaDTOGet>>> ObtenerMesas(int idMozo)
        {
            var estados = await _context.Estado.ToListAsync();
           
            var mesas = await _context.Mesa.Where(h => h.MozoId == idMozo)
                .OrderBy(h => h.Nombre)
                .ToListAsync();


            var rta = mesas.Join(estados, 
                mesa => mesa.Estado,
                estado => estado.Id,
                (mesa, estado) => new MesaDTOGet(mesa.Nombre, mesa.Numero, mesa.MozoId, mesa.QR, mesa.Estado, estado.Nombre)).OrderBy(h => h.Nombre)
                .ToList();


            return new Response<List<MesaDTOGet>>(rta,200);           
        }

        public async Task<Response<(string,int)>> LlamarMozo(int idMesa)
        {
            var mesa = await _context.Mesa.FirstOrDefaultAsync(h => h.Id == idMesa);
            if (mesa == null)
            {
                return new Response<(string, int)>(("No se encontró la mesa",0), 404);
            }

            mesa.Estado = 4; // "Llamar Mozo"
            int respuesta = await _context.SaveChangesAsync();
        
            var mensaje = respuesta > 0 ? "Llamado de mozo registrado con éxito" : "Ha ocurrido un error al registrar el llamado de mozo.";

            return new Response<(string, int)>((mensaje, mesa.Numero), 200);

        }
    }
}
