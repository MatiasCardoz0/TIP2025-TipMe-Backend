using System.ComponentModel.DataAnnotations.Schema;

namespace TipMeBackend.Models
{
    public class NotaMesa
    {
        [Column("NOTA_MOZO_ID")]
        public int MozoId { get; set; }
        [Column("NOTA_MESA_ID")]
        public int MesaId { get; set; }
        [Column("NOTA_RENGLON")]
        public int Renglon {  get; set; }
        [Column("NOTA_MENSAJE")]
        public string Mensaje {  get; set; }

        public NotaMesa(int mozoId, int mesaId, int renglon, string mensaje)
        {
            MozoId = mozoId;
            MesaId = mesaId;
            Renglon = renglon;
            Mensaje = mensaje;
        }
    }
}
