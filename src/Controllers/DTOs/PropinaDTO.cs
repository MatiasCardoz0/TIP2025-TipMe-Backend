using System.ComponentModel.DataAnnotations.Schema;

namespace TipMeBackend.Controllers.DTOs
{
    public class PropinaDTO
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMesa { get; set; }
        //public int IdMozo { get; set; }
    }
}
