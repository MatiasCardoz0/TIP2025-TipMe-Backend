namespace TipMeBackend.Controllers.DTOs
{
    public class PropinaDTOGet
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMesa { get; set; }
        public int IdMozo { get; set; }
        public string NombreMesa { get; set; }
        public int NumeroMesa { get; set; }

        public PropinaDTOGet(decimal monto, DateTime fecha, int idMesa, int idMozo, string nombreMesa, int numeroMesa)
        {
            Monto = monto;
            Fecha = fecha;
            IdMesa = idMesa;
            IdMozo = idMozo;
            NombreMesa = nombreMesa;
            NumeroMesa = numeroMesa;
        }
    }
}
