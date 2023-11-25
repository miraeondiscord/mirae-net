using MiraeNet.Discord.Networking.Gateway;

namespace MiraeNet.Discord.Gateway;

public partial class GatewayClient
{
    private Timer? _heartbeatTimer;

    private void StartHeartbeatTimer(int heartbeatInterval)
    {
        _heartbeatTimer = new Timer(SendHeartbeat, default, 0, heartbeatInterval);
    }

    private void SendHeartbeat(object? state)
    {
        var payload = new OutgoingPayload<int>
        {
            OpCode = 1,
            Data = _lastSequenceIndex
        };
        _ = SendPayloadAsync(payload);
    }
}