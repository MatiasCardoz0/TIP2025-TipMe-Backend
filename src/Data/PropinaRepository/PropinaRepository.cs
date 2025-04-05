using Microsoft.EntityFrameworkCore;
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
            _context.Add(propina);
            int result = await _context.SaveChangesAsync();
            return new Response<string>(result > 0? "Registro realizado con éxito" : "Ha ocurrido un error al grabar", result > 0? 200 : 400);           
        }

        public async Task<Response<List<Propina>>> ObtenerPropinas(int idMozo)
        {
            List<Propina> propinas = await _context.Propina.Where(e => e.IdMozo == idMozo).ToListAsync();
            return new Response<List<Propina>>(propinas, 200);
        }
    }
}
