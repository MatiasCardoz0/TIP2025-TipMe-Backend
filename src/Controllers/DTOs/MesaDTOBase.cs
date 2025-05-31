namespace TipMeBackend.Controllers.DTOs
{
    public class MesaDTOBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public int MozoId { get; set; }
        public string QR { get; set; }
        public int Estado { get; set; }
        public string NombreEstado { get; set; }
        public decimal PosicionX { get; set; }
        public decimal PosicionY { get; set; }

        public MesaDTOBase(int id, string nombre, int numero, int mozoId, string qR, int estado, string nombreEstado, decimal posicionX, decimal posicionY)
        {
            Id = id;
            Nombre = nombre;
            Numero = numero;
            MozoId = mozoId;
            QR = qR;
            Estado = estado;
            NombreEstado = nombreEstado;
            PosicionX = posicionX;
            PosicionY = posicionY;
        }
    }
}
