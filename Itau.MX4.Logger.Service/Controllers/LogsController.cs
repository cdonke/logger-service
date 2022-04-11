using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using Itau.MX4.Logger.Service.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Itau.MX4.Logger.Service.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private readonly LogCollection _logCollection;

        public LogsController(LogCollection logCollection, ILogger<LogsController> logger)
        {
            _logCollection = logCollection;
        }

        [HttpGet("ws")]
        public async Task GetWebSocket()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await ReceberMensagem(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
        private async Task ReceberMensagem(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var stringBuilder = new StringBuilder(buffer.Length, Int32.MaxValue);
            WebSocketReceiveResult receiveResult;

            do
            {
                receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var receivedContent = System.Text.UTF8Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                stringBuilder.Append(receivedContent);

                if (receiveResult.EndOfMessage)
                {
                    _logCollection.Enqueue(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
            }
            while (!receiveResult.CloseStatus.HasValue);

            await webSocket.CloseAsync(receiveResult.CloseStatus.Value, receiveResult.CloseStatusDescription, CancellationToken.None);
        }


        [HttpPost("{servico}/Start")]
        public void StartService([FromRoute] string servico, [FromBody] LogEntity logEntity)
        {
            logEntity.ApplicationName = servico;
            _logCollection.Enqueue(logEntity);
        }
        [HttpPost("{servico}/Stop")]
        public void StopService([FromRoute] string servico, [FromBody] LogEntity logEntity)
        {
            logEntity.ApplicationName = servico;
            _logCollection.Enqueue(logEntity);
        }

        [HttpPost("{servico}/{logLevel}")]
        public void Log([FromRoute] string servico, [FromRoute] LogLevel logLevel, [FromBody] LogEntity logEntity)
        {
            logEntity.ApplicationName = servico;
            logEntity.Level = logLevel;

            _logCollection.Enqueue(logEntity);
        }
    }
}
