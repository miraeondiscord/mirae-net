namespace MiraeNet.Core.Discord;

/// <summary>
///     Defines the API for a service that raises Discord gateway events.
///     For more information, see the
///     <a href="https://discord.com/developers/docs/topics/gateway-events#receive-events">
///         Discord Developer Documentation
///     </a>
///     .
/// </summary>
public interface IEventService
{
    /// <summary>
    ///     A new message was created in a channel.
    /// </summary>
    event Action<Message> MessageCreated;
}