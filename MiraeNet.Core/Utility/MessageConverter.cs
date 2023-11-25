using MiraeNet.Core.Completion;
using MiraeNet.Core.Discord;

namespace MiraeNet.Core.Utility;

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
}