using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Gateway;

public class GatewayHeartbeat
{
    private Timer? _heartbeatTimer;
    private readonly GatewayContext _context;

    public GatewayHeartbeat(GatewayContext context)
    {
        _context = context;
        _context.SubscribeToStateChanges(OnStateChange);
    }

    private void SendHeartbeat(object? state)
    {
        _context.Logger.LogInformation("Heartbeat.");
    }

    private void OnStateChange(GatewayClientState newState)
    {
        // Start a heartbeat, if not already, when the client is
        // in a handshaken state.
        if (newState is GatewayClientState.Unidentified or GatewayClientState.Ready)
        {
            if (_heartbeatTimer is not null)
                return;

            _heartbeatTimer = new Timer(SendHeartbeat, default, 0, 1000);
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