using Microsoft.EntityFrameworkCore;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Data.PropinaRepository
{
    public class PropinaRepository : IPropinaRepository
    {
        private readonly Context _context;

        public PropinaRepository(Context context)
        {
            _context = context;
        }

        public async Task<Response<string>> GrabarPropina(Propina propina)
        {
            var mozo = await _context.Mesa.Where(m => m.Id == propina.Id).FirstOrDefaultAsync();

            if(mozo == null)
            {
                return new Response<string>( "No existe el mozo para la mesa especificada.", 500);
            }

            propina.IdMozo = mozo.Id;

            _context.Add(propina);
            int result = await _context.SaveChangesAsync();
            return new Response<string>(result > 0? "Registro realizado con éxito" : "Ha ocurrido un error al grabar", result > 0? 200 : 400);           
        }

        public async Task<Response<List<PropinaDTOGet>>> ObtenerPropinas(int idMozo)
        {            
            List<Propina> propinas = await _context.Propina.Where(e => e.IdMozo == idMozo).ToListAsync();

            var mesas = await _context.Mesa.Where(h => h.MozoId == idMozo)
               .OrderBy(h => h.Nombre)
               .ToListAsync();

            var join = propinas.Join(mesas,
                propina => propina.IdMesa,
                mesa => mesa.Id,
                (propina, mesa) => new PropinaDTOGet(propina.Monto, propina.Fecha, propina.IdMesa, propina.IdMozo, mesa.Nombre, mesa.Numero)
                ).ToList();

            return new Response<List<PropinaDTOGet>>(join, 200);
        }
    }
}
