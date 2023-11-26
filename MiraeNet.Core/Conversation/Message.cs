namespace MiraeNet.Core.Conversation;

/// <summary>
///     Represents a message. A collection of these form a conversation.
///     This differs from <see cref="Discord.Message" /> in the Discord namespace,
///     as that class represents a message that originates from the Discord client
///     and does not contain all the information needed for the agent.
/// </summary>
public class Message
{
    public string Content { get; set; }
}