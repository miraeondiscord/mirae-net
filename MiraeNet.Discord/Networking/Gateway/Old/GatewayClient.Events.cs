using Microsoft.Extensions.Logging;
using MiraeNet.Core.Discord;
using MiraeNet.Discord.Networking.Gateway;

namespace MiraeNet.Discord.Gateway;

public partial class GatewayClient
{
    private static Dictionary<string, Action<IncomingPayload>> GetEventHandlers(GatewayClient client)
    {
        return new Dictionary<string, Action<IncomingPayload>>
        {
            { "READY", client.HandleReadyEvent },
            { "MESSAGE_CREATE", client.HandleMessageCreateEvent }
        };
    }

    private void HandleEventPayload(IncomingPayload payload)
    {
        _eventHandlers.TryGetValue(payload.EventName!, out var eventHandler);
        eventHandler?.Invoke(payload);
    }

    private void HandleReadyEvent(IncomingPayload payload)
    {
        _logger.LogInformation("Gateway connection is ready.");
        _state = GatewayClientState.Open;
        Readied?.Invoke();
    }

    private void HandleMessageCreateEvent(IncomingPayload payload)
    {
        var message = payload.GetData<Message>();
        MessageCreated?.Invoke(message);
    }
}