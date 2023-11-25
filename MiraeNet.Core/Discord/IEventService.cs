namespace MiraeNet.Core.Discord;

// TODO: Full spec coverage

/// <summary>
///     Defines the contract for a service that emits Discord gateway events.
///     Please note that this is an incomplete definition.
/// </summary>
public interface IEventService
{
    /// <summary>
    ///     An event that is raised when the Discord gateway connection has opened.
    /// </summary>
    event Action Opened;

    /// <summary>
    ///     An event that is raised when the Discord gateway connection becomes ready.
    /// </summary>
    event Action Readied;

    /// <summary>
    ///     An event that is raised when the Discord gateway connection gets interrupted.
    /// </summary>
    event Action Reconnecting;

    /// <summary>
    ///     An event that is raised when the Discord gateway connection has closed.
    /// </summary>
    event Action Closed;

    /// <summary>
    ///     An event that is raised when a new message is created in a Discord channel.
    /// </summary>
    event Action<Message> MessageCreated;
}