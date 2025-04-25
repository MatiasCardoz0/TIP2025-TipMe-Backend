namespace TipMeBackend.Models
{
    public class Mozo
    {
        public class Waiter
        {
            public int Mozo_Id { get; set; }
            public string Mozo_Nombre { get; set; }
            public string Mozo_Apellido { get; set; }
            public string Mozo_DNI { get; set; }
            public string Mozo_CVU { get; set; }
            public string Mozo_Alias { get; set; }
            public string PushToken { get; set; } // Token de Expo para poder recibir notificaciones push
        }
    }
}
