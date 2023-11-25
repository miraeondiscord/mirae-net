using System.Globalization;
using MiraeNet.Core.Discord;
using Moq;

namespace MiraeNet.Discord.Mock;

public class MockChannelService : Mock<IChannelService>
{
    private readonly int _delay;

    public MockChannelService(int delay = 1000)
    {
        _delay = delay;
        Setup(c => c.GetChannelAsync(It.IsAny<string>()))
            .Callback(GetChannelAsync);
        Setup(c => c.ModifyChannelAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, dynamic>>()))
            .Callback(ModifyChannelAsync);
        Setup(c => c.DeleteChannelAsync(It.IsAny<string>()))
            .Callback(DeleteChannelAsync);
        Setup(c => c.CreateMessageAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Callback(CreateMessageAsync);
        Setup(c => c.TriggerTypingIndicatorAsync(It.IsAny<string>()))
            .Callback(TriggerTypingIndicatorAsync);
    }

    private async Task<Channel> GetChannelAsync(string channelId)
    {
        await Task.Delay(_delay);
        return new Channel
        {
            Id = channelId,
            Name = "general",
            Type = ChannelType.GuildText
        };
    }

    private async Task<Channel> ModifyChannelAsync(string channelId, Dictionary<string, dynamic> modifications)
    {
        await Task.Delay(_delay);
        return new Channel
        {
            Id = channelId,
            Name = "general",
            Type = ChannelType.GuildText
        };
    }

    private async Task<Channel> DeleteChannelAsync(string channelId)
    {
        await Task.Delay(_delay);
        return new Channel
        {
            Id = channelId,
            Name = "general",
            Type = ChannelType.GuildText
        };
    }

    private async Task<Message> CreateMessageAsync(string channelId, string content)
    {
        // TODO: Convert to a reusable User dummy
        await Task.Delay(_delay);
        return new Message
        {
            Id = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            Content = content,
            ChannelId = channelId,
            Author = new User
            {
                Id = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Username = "cooluser",
                Discriminator = "",
                IsBot = false,
                IsSystem = false,
                HasMfaEnabled = false,
                Locale = "en-US",
                IsVerified = false,
                Flags = 0,
                PremiumType = 0,
                PublicFlags = 0
            },
            Mentions = new List<User>(),
            MentionRoles = new List<string>(),
            MentionChannels = new List<ChannelMention>(),
            Attachments = new List<Attachment>()
        };
    }

    private Task TriggerTypingIndicatorAsync(string channelId)
    {
        return Task.Delay(_delay);
    }
}