using MiraeNet.Core.Discord;
using MiraeNet.Discord.Gateway;

namespace MiraeNet.Discord;

/// <summary>
///     A service that emits Discord gateway events.
///     Please note that this is an incomplete definition.
/// </summary>
public class EventService : IEventService
{
    public EventService(GatewayClient gateway)
    {
        gateway.Opened += () => { Opened?.Invoke(); };
        gateway.Readied += () => { Readied?.Invoke(); };
        gateway.Reconnecting += () => { Reconnecting?.Invoke(); };
        gateway.Closed += () => { Closed?.Invoke(); };
        gateway.MessageCreated += message => { MessageCreated?.Invoke(message); };
    }

    public event Action? Opened;
    public event Action? Readied;
    public event Action? Reconnecting;
    public event Action? Closed;
    public event Action<Message>? MessageCreated;
}