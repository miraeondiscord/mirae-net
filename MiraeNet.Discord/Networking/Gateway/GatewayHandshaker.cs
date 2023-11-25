using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Gateway;

public class GatewayHandshaker
{
    private readonly GatewayClient _client;

    public GatewayHandshaker(GatewayClient client)
    {
        _client = client;
        _client.SubscribeToOpCode(10, OnGatewayHello);
    }

    private void OnGatewayHello(IncomingPayload payload, WebSocketReceiveResult message)
    {
        var data = payload.GetData<Dictionary<string, JsonElement>>();
        var heartbeatInterval = data["heartbeat_interval"].GetInt32();
        _client.Logger.LogInformation("Heartbeat Interval: {interval}", heartbeatInterval);
        _client.Heartbeat.HeartbeatInterval = heartbeatInterval;
        _client.State = GatewayClientState.Unidentified;
    }
}