using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Gateway;

public class GatewayHeartbeat
{
    private readonly GatewayClient _client;
    private Timer? _heartbeatTimer;

    public GatewayHeartbeat(GatewayClient client)
    {
        _client = client;
        _client.SubscribeToStateChanges(OnStateChange);
    }

    public int HeartbeatInterval { get; set; }

    private void SendHeartbeat(object? state)
    {
        _client.Logger.LogInformation("Heartbeat.");
    }

    private void OnStateChange(GatewayClientState newState)
    {
        // Start a heartbeat, if not already, when the client is
        // in a handshaken state.
        if (newState is GatewayClientState.Unidentified or GatewayClientState.Ready)
        {
            if (_heartbeatTimer is not null)
                return;

            _heartbeatTimer = new Timer(SendHeartbeat, default, 0, HeartbeatInterval);
        }

        // Stop a heartbeat, if there is one, when the client is
        // in a disconnected state.
        if (newState is GatewayClientState.Closed or GatewayClientState.Closing or GatewayClientState.Reconnecting)
        {
            if (_heartbeatTimer is null)
                return;

            _heartbeatTimer.Dispose();
            _heartbeatTimer = null;
        }
    }
}