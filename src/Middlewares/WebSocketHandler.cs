namespace TipMeBackend.Middlewares
{
    using System.Net.WebSockets;
    using System.Text;

    public class WebSocketHandler
    {
        //un diccionario para cada tipo de usuario, uno para los clientes y otro para los mozos. se asocian por el id de la mesa.

        private static readonly Dictionary<int, WebSocket> ClientesSockets = new(); //int = idMesa o idCliente
        private static readonly Dictionary<int, WebSocket> MozosSockets = new(); //int = idMozo

        public async Task HandleConnectionAsync(HttpContext context, WebSocket webSocket, int id, bool esMozo)
        {
            var UserSockets = esMozo ? MozosSockets : ClientesSockets;

            //lock se usa para evitar que dos conexiones se asocien al mismo usuario.
            //usersockets es un diccionario que almacena las conexiones de los usuarios.

            // Asocia el WebSocket con un usuario específico
            lock (UserSockets)
            {
                UserSockets[id] = webSocket;
            }
            try
            {
                Console.WriteLine($"Nueva conexión asociada a {(esMozo ? "userId" : "mesaId")}: {id}, esMozo?: {esMozo}");
                await HandleMessagesAsync(webSocket);
            }
            finally
            {
                // Elimina la conexión cuando se cierra
                lock (UserSockets)
                {
                    UserSockets.Remove(id);
                }
            }
        }

        private async Task HandleMessagesAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];

            //Mientras el estado de la conexion sea abierto, los proceso
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    // Proceso el mensaje recibido
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    // Envío respuesta al cliente
                    var responseMessage = Encoding.UTF8.GetBytes($"Servidor recibió: {receivedMessage}");
                    await webSocket.SendAsync(new ArraySegment<byte>(responseMessage), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    // Cierro la conexion
                    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                }
            }
        }

        public static async Task SendMessageToClienteAsync(int userId, string message)
        {
            WebSocket? socket;
            lock (ClientesSockets)
            {
                ClientesSockets.TryGetValue(userId, out socket);
            }

            if (socket != null && socket.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public static async Task SendMessageToMozoAsync(int userId, string message)
        {
            WebSocket? socket;
            lock (MozosSockets)
            {
                MozosSockets.TryGetValue(userId, out socket); //out es una palabra reservada que indica que la variable socket se inicializa con el valor de MozosSockets[userId]
            }

            if (socket != null && socket.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
