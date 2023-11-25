using System.Text.Json;
using MiraeNet.Discord.Networking.Gateway;

namespace MiraeNet.Discord.Gateway;

public partial class GatewayClient
{
    private static Dictionary<int, Action<IncomingPayload>> GetOpCodeHandlers(GatewayClient client)
    {
        return new Dictionary<int, Action<IncomingPayload>>
        {
            { 0, client.HandleDispatchOpCode },
            { 10, client.HandleHelloOpCode }
        };
    }

    private void HandleIncomingPayload(IncomingPayload payload)
    {
        if (payload.SequenceIndex is not null) _lastSequenceIndex = payload.SequenceIndex.Value;

        _opCodeHandlers.TryGetValue(payload.OpCode, out var opCodeHandler);
        opCodeHandler?.Invoke(payload);
    }

    private void HandleDispatchOpCode(IncomingPayload payload)
    {
        HandleEventPayload(payload); // Defined in GatewayClient.Events.cs.
    }

    private void HandleHelloOpCode(IncomingPayload payload)
    {
        var data = payload.GetData<Dictionary<string, JsonElement>>();
        var heartbeatInterval = data["heartbeat_interval"].GetInt32();
        StartHeartbeatTimer(heartbeatInterval); // Defined in GatewayClient.Heartbeat.cs.
        _state = GatewayClientState.Unidentified;
    }
}