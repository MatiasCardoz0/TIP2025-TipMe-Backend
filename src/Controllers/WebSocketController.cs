﻿using Microsoft.AspNetCore.Mvc;
using TipMeBackend.Middlewares;

namespace TipMeBackend.Controllers
{
    [ApiController]
    [Route("api/")]
    public class WebSocketController : ControllerBase
    {
        private readonly WebSocketHandler _webSocketHandler;

        public WebSocketController(WebSocketHandler webSocketHandler)
        {
            _webSocketHandler = webSocketHandler;
        }

        [HttpGet("connect")]
        public async Task ConnectWebSocket([FromQuery] int userId, [FromQuery] bool esMozo)
        {
            //Verifico que sea una conexion de websocket
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await _webSocketHandler.HandleConnectionAsync(HttpContext, webSocket, userId, esMozo);
            }
            else
            // Sino bad request porque no es solicitud webSocket
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}
