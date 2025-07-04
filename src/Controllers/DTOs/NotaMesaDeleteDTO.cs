using System.ComponentModel.DataAnnotations.Schema;

namespace TipMeBackend.Controllers.DTOs
{
    public class NotaMesaDeleteDTO
    {
        public int MesaId { get; set; }
        public int Renglon { get; set; }
    }
}
