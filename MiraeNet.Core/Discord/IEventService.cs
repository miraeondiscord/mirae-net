namespace MiraeNet.Core.Discord;

// TODO: Full spec coverage

/// <summary>
///     Defines the contract for a service that emits Discord gateway events.
///     Please note that this is an incomplete definition.
/// </summary>
public interface IEventService
{
    /// <summary>
    ///     An event that is raised when a new message is created in a Discord channel.
    /// </summary>
    event Action<Message> MessageCreated;
}