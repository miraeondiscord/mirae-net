using MiraeNet.Core.Completion;
using MiraeNet.Core.Discord;

namespace MiraeNet.Core.Utility;

/// <summary>
/// Defines utility methods for converting between <see cref="Message"/>
/// and <see cref="CompletionMessage"/> objects.
/// </summary>
public static class MessageConverter
{
    public static CompletionMessage Convert(Message message, string assistantUserId)
    {
        var role = message.Author.Id == assistantUserId ? CompletionMessageRole.Assistant : CompletionMessageRole.User;
        var newMessage = new CompletionMessage
        {
            Name = message.Author.Username,
            Role = role,
            Content = message.Content ?? ""
        };
        return newMessage;
    }

    public static List<CompletionMessage> Convert(IList<Message> messages, string assistantUserId, string systemPrompt)
    {
        var newMessages = new List<CompletionMessage>();
        var systemMessage = new CompletionMessage
        {
            Role = CompletionMessageRole.System,
            Content = systemPrompt
        };
        newMessages.Add(systemMessage);
        newMessages.AddRange(messages.Select(message => Convert(message, assistantUserId)));
        return newMessages;
    }
}