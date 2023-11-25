using MiraeNet.Core.Discord;

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
        _state = GatewayClientState.Open;
    }

    private void HandleMessageCreateEvent(IncomingPayload payload)
    {
        var message = payload.GetData<Message>();
        // OnMessageCreated?.Invoke(message);
    }
}