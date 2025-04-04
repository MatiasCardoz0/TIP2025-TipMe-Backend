using Microsoft.EntityFrameworkCore;
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

        public async Task<Response<List<Mesa>>> ObtenerMesas(int idMozo)
        {
            var rta = await _context.Mesa
                .Where(h => h.MozoId == idMozo)
                .OrderBy(h => h.Nombre)
                .ToListAsync(); ;

            return new Response<List<Mesa>>(rta,200);           
        }
    }
}
