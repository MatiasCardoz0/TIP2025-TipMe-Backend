using Microsoft.EntityFrameworkCore;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Data.NotaRepository
{
    public class NotaRepository : INotaRepository
    {
        private readonly Context _context;

        public NotaRepository(Context context)
        {
            _context = context;
        }

        public async Task<Response<string>> ActualizarNotaMesa(NotaMesaPutDTO notaDTO)
        {
            var mesa = await _context.Mesa.Where(e => e.Id == notaDTO.MesaId).FirstOrDefaultAsync();

            if (mesa == null) return new Response<string>("No existe la mesa especificada.", 500);

            NotaMesa nota = await _context.Nota.Where(n => n.MesaId == mesa.Id && n.Renglon == notaDTO.Renglon && n.MozoId == mesa.MozoId).FirstOrDefaultAsync();

            if (nota == null) return new Response<string>("No existe la nota para la mesa especificada.", 500);

            nota.Mensaje = notaDTO.Mensaje;

            _context.Update(nota);
            int result = await _context.SaveChangesAsync();

            return new Response<string>(result > 0 ? "Actualización realizada con éxito" : "Ha ocurrido un error al actualizar", result > 0 ? 200 : 400);
        }

        public async Task<Response<string>> BorrarNotaMesa(int idMesa, int renglonMesa)
        {
            var mesa = await _context.Mesa.Where(e => e.Id == idMesa).FirstOrDefaultAsync();

            if (mesa == null) return new Response<string>("No existe la mesa especificada.", 500);

            NotaMesa notaABorrar = await _context.Nota.Where(n => n.MesaId == mesa.Id && n.Renglon == renglonMesa).FirstOrDefaultAsync();

            if (notaABorrar == null) return new Response<string>("No existe la nota para la mesa especificada.", 500);

            _context.Remove(notaABorrar);
            int result = await _context.SaveChangesAsync();

            return new Response<string>(result > 0 ? "Borrado realizado con éxito" : "Ha ocurrido un error al borrar", result > 0 ? 200 : 400);
        }

        public async Task<Response<string>> BorrarTodasLasNotas(int idMesa)
        {
            var mesa = await _context.Mesa.Where(e => e.Id == idMesa).FirstOrDefaultAsync();

            if (mesa == null) return new Response<string>("No existe la mesa especificada.", 500);

            List<NotaMesa> notasABorrar = await _context.Nota.Where(n => n.MesaId == mesa.Id).ToListAsync();

            _context.RemoveRange(notasABorrar);
            int result = await _context.SaveChangesAsync();

            return new Response<string>(result > 0 ? "Borrado realizado con éxito" : "Ha ocurrido un error al borrar", result > 0 ? 200 : 400);
        }

        public async Task<Response<string>> GrabarNotaMesa(NotaMesaPostDTO nota)
        {
            var mesa = await _context.Mesa.Where(e => e.Id == nota.MesaId).FirstOrDefaultAsync();

            if (mesa == null) return new Response<string>("No existe la mesa especificada.", 500);

            var hayItem = await _context.Nota.Where(e => e.MesaId == mesa.Id).AnyAsync();

            int ultimoRenglon = 0;

            if(hayItem) ultimoRenglon = await _context.Nota.Where(n => n.MesaId == mesa.Id).MaxAsync(e => e.Renglon);

            NotaMesa nuevaNota = new NotaMesa(mesa.MozoId, mesa.Id, ultimoRenglon + 1, nota.Mensaje);

            _context.Add(nuevaNota);
            int result = await _context.SaveChangesAsync();

            return new Response<string>(result > 0 ? "Registro realizado con éxito" : "Ha ocurrido un error al grabar", result > 0 ? 200 : 400);
        }

        public async Task<Response<List<NotaMesa>>> ObtenerNotasDeMesa(int idMesa)
        {
            List<NotaMesa> notas = new List<NotaMesa>();

            var mesa = await _context.Mesa.Where(e => e.Id == idMesa).FirstOrDefaultAsync();

            if(mesa != null) notas = await _context.Nota.Where(n => n.MesaId == mesa.Id).ToListAsync();

            return new Response<List<NotaMesa>>(notas, 200);
        }
    }
}
