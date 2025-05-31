using System.ComponentModel.DataAnnotations.Schema;

namespace TipMeBackend.Controllers.DTOs
{
    public class MesaDTO
    {
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public int MozoId { get; set; }
        public string QR { get; set; }
        public int Estado { get; set; }
        public decimal PosicionX { get; set; }
        public decimal PosicionY { get; set; }
    }
}
