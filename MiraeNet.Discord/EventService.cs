using System.Net.WebSockets;
using Microsoft.Extensions.Logging;
using MiraeNet.Core.Discord;
using MiraeNet.Discord.Networking.Gateway;

namespace MiraeNet.Discord;

/// <summary>
///     A service that emits Discord gateway events.
///     Please note that this is an incomplete definition.
/// </summary>
public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;

    public EventService(GatewayClient gateway, ILogger<EventService> logger)
    {
        _logger = logger;
        gateway.SubscribeToEvent("MESSAGE_CREATE", OnGatewayMessageCreated);
    }

    public event Action<Message>? MessageCreated;

    private void OnGatewayMessageCreated(IncomingPayload payload, WebSocketReceiveResult wsMessage)
    {
        var message = payload.GetData<Message>();
        _logger.LogInformation("Handling message creation. - Author: {author}", message.Author.Username);
        MessageCreated?.Invoke(message);
    }
}