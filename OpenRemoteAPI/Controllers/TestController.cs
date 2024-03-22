using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace OpenRemoteAPI.Controllers;

public class TestController : ControllerBase
{
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var bytes = Encoding.UTF8.GetBytes("test");
            await webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}