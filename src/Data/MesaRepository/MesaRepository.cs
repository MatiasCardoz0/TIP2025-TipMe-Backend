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

        public async Task<Response<List<MesaDTOBase>>> ObtenerMesas(int idMozo)
        {
            var estados = await _context.Estado.ToListAsync();
           
            var mesas = await _context.Mesa.Where(h => h.MozoId == idMozo)
                .OrderBy(h => h.Nombre)
                .ToListAsync();


            var rta = mesas.Join(estados, 
                mesa => mesa.Estado,
                estado => estado.Id,
                (mesa, estado) => new MesaDTOBase(mesa.Id, mesa.Nombre, mesa.Numero, mesa.MozoId, mesa.QR, mesa.Estado, estado.Nombre, mesa.PosicionX, mesa.PosicionY)).OrderBy(h => h.Nombre)
                .ToList();


            return new Response<List<MesaDTOBase>>(rta,200);           
        }

        public async Task<Response<(string,int)>> LlamarMozo(int idMesa)
        {
            var mesa = await _context.Mesa.FirstOrDefaultAsync(h => h.Id == idMesa);
            if (mesa == null)
            {
                return new Response<(string, int)>(("No se encontró la mesa",0), 404);
            }

            int respuesta = await CambiarEstadoMesa(idMesa, 4); // "Llamar Mozo"
        
            var mensaje = respuesta > 0 ? "Llamado de mozo registrado con éxito" : "Ha ocurrido un error al registrar el llamado de mozo.";

            return new Response<(string, int)>((mensaje, mesa.Numero), 200);

        }

        public async Task<Response<(string,int)>> PedirCuenta(int idMesa)
        {
            var mesa = await _context.Mesa.FirstOrDefaultAsync(h => h.Id == idMesa);
            if (mesa == null)
            {
                return new Response<(string, int)>(("No se encontró la mesa",0), 404);
            }
         
            int respuesta = await CambiarEstadoMesa(idMesa, 8); // "Pedir Cuenta"

            var mensaje = respuesta > 0 ? "Pedido de cuenta registrado con éxito" : "Ha ocurrido un error al registrar el pedido de cuenta.";

            return new Response<(string, int)>((mensaje, mesa.Numero), 200);
        }


        public async Task<int> CambiarEstadoMesa(int idMesa, int estado)
        {
            var mesa = await _context.Mesa.FirstOrDefaultAsync(h => h.Id == idMesa);
            if (mesa == null)
            {
                return 0;
            }
            mesa.Estado = estado;

            return await _context.SaveChangesAsync();
        }

        public async Task<Response<string>> ActualizarMesa(Mesa mesa, string nombreEstado)
        {
            Mesa mesaDB = await _context.Mesa.AsQueryable().Where(e => e.Id == mesa.Id).FirstOrDefaultAsync();

            if (mesaDB != null)
            {
                EstadoMesa estado = await _context.Estado.AsQueryable().Where(e => e.Nombre == nombreEstado).FirstOrDefaultAsync();

                mesaDB.QR = mesa.QR;
                mesaDB.Numero = mesa.Numero;
                mesaDB.Estado = estado.Id;
                mesaDB.PosicionX = mesa.PosicionX;
                mesaDB.PosicionY = mesa.PosicionY;
                mesaDB.Nombre = mesa.Nombre;

                _context.Mesa.Update(mesaDB);

                int respuesta = await _context.SaveChangesAsync();

                return new Response<string>(respuesta > 0 ? "Actualización realizada con éxito" : "Ha ocurrido un error al realizar la actualización.", respuesta > 0 ? 200 : 400);
            }
            else
            {
                return new Response<string>("Mesa inexistente", 404);
            }
        }

        public async Task<Response<string>> BorrarMesa(int idMesa)
        {
            Mesa mesaDB = await _context.Mesa.AsQueryable().Where(e => e.Id == idMesa).FirstOrDefaultAsync();

            if(mesaDB != null)
            {
                _context.Mesa.Remove(mesaDB);

                int respuesta = await _context.SaveChangesAsync();

                return new Response<string>(respuesta > 0 ? "Mesa eliminada con éxito" : "Ha ocurrido un error al eliminar la mesa.", respuesta > 0 ? 200 : 400);
            }
            else
            {
                return new Response<string>("Mesa inexistente", 404);
            }
        }
    }
}
