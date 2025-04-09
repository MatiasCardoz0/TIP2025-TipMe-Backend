using System.ComponentModel.DataAnnotations.Schema;

namespace TipMeBackend.Models
{
    public class EstadoMesa
    {
        [Column("ESTADO_ID")]
        public int Id { get; set; }
        [Column("ESTADO_NOMBRE")]
        public string Nombre { get; set; }
        [Column("ESTADO_DESCRIPCION")]
        public string Descripcion { get; set; }

        public EstadoMesa(int id, string nombre, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }
    }
}
