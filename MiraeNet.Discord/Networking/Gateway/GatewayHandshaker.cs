using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Gateway;

public class GatewayHandshaker
{
    private readonly GatewayContext _context;

    public GatewayHandshaker(GatewayContext context)
    {
        _context = context;
        _context.SubscribeToOpCode(10, OnGatewayHello);
    }

    private void OnGatewayHello(IncomingPayload payload, WebSocketReceiveResult message)
    {
        var data = payload.GetData<Dictionary<string, JsonElement>>();
        var heartbeatInterval = data["heartbeat_interval"].GetInt32();
        _context.Logger.LogInformation("Heartbeat Interval: {interval}", heartbeatInterval);
        _context.State = GatewayClientState.Unidentified;
    }
}