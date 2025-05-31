using System.ComponentModel.DataAnnotations.Schema;

namespace TipMeBackend.Models
{
    public class Mesa
    {
        [Column("MESA_ID")]
        public int Id { get; set; }
        [Column("MESA_NOMBRE")]
        public string Nombre { get; set; }
        [Column("MESA_INUMERO")]
        public int Numero { get; set; }
        [Column("MESA_MOZO")]
        public int MozoId { get; set; }
        [Column("MESA_QR")]
        public string QR {  get; set; }
        [Column("MESA_ESTADO")]
        public int Estado { get; set; }
        [Column("MESA_POSICION_X")]
        public decimal PosicionX { get; set; }
        [Column("MESA_POSICION_Y")]
        public decimal PosicionY { get; set; }

        public Mesa() { }

        public Mesa (int id, string nombre, int numero, int mozoId, string qR, int estado, decimal posicionX, decimal posicionY)
        {
            Id = id;
            Nombre = nombre;
            Numero = numero;
            MozoId = mozoId;
            QR = qR;
            Estado = estado;
            PosicionX = posicionX;
            PosicionY = posicionY;
        }
    }
}
