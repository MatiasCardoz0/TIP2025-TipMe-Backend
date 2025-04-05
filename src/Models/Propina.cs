using System.ComponentModel.DataAnnotations.Schema;

namespace TipMeBackend.Models
{
    public class Propina
    {
        [Column("PROP_ID")]
        public int Id { get; set; }
        [Column("PROP_MONTO")]
        public decimal Monto { get; set; }
        [Column("PROP_FECHA")]
        public DateTime Fecha { get; set; }
        [Column("PROP_ID_MESA")]
        public int IdMesa { get; set; }
        [Column("PROP_ID_MOZO")]
        public int IdMozo { get; set; }

        public Propina() { }

        public Propina(int id, decimal monto, DateTime fecha, int idMesa, int idMozo)
        {
            
            Id = id;
            Monto = monto;
            Fecha = fecha;
            IdMesa = idMesa;
            IdMozo = idMozo;
            
        }
    }
}
