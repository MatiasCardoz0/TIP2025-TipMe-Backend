namespace TipMeBackend.Middlewares
{
    using System.Net.WebSockets;
    using System.Text;

    public class WebSocketHandler
    {
        public async Task HandleConnectionAsync(HttpContext context, WebSocket webSocket)
        {                      
            await HandleMessagesAsync(webSocket);
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
    }
}
