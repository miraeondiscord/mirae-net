using MiraeNet.Core.Discord;

namespace MiraeNet.Discord;

/// <summary>
///     A service that emits Discord gateway events.
///     Please note that this is an incomplete definition.
/// </summary>
public class EventService : IEventService
{
    public event Action? Opened;
    public event Action? Readied;
    public event Action? Reconnecting;
    public event Action? Closed;
    public event Action? MessageCreated;
}