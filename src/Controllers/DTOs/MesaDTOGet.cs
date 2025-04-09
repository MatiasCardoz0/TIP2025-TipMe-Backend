namespace TipMeBackend.Controllers.DTOs
{
    public class MesaDTOGet
    {
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public int MozoId { get; set; }
        public string QR { get; set; }
        public int Estado { get; set; }
        public string NombreEstado { get; set; }

        public MesaDTOGet(string nombre, int numero, int mozoId, string qR, int estado, string nombreEstado)
        {
            Nombre = nombre;
            Numero = numero;
            MozoId = mozoId;
            QR = qR;
            Estado = estado;
            NombreEstado = nombreEstado;
        }
    }
}
