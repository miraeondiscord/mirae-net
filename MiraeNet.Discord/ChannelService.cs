using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using MiraeNet.Core.Discord;
using MiraeNet.Discord.Networking.Rest;

namespace MiraeNet.Discord;

public class ChannelService(RestClient rest, ILogger<ChannelService> logger) : IChannelService
{
    public Task<Channel> GetChannelAsync(string channelId)
    {
        throw new NotImplementedException();
    }

    public Task<Channel> ModifyChannelAsync(string channelId, Dictionary<string, dynamic> modifications)
    {
        throw new NotImplementedException();
    }

    public Task<Channel> DeleteChannelAsync(string channelId)
    {
        throw new NotImplementedException();
    }

    public async Task<Message> CreateMessageAsync(string channelId, string content)
    {
        logger.LogInformation("Creating new message in channel: {id}", channelId);
        var requestData = new Dictionary<string, dynamic>
        {
            { "content", content }
        };
        var response = await rest.Http.PostAsJsonAsync($"channels/{channelId}/messages", requestData);
        response.EnsureSuccessStatusCode();
        var createdMessage = await response.Content.ReadFromJsonAsync<Message>();
        return createdMessage!;
    }

    public Task TriggerTypingIndicatorAsync(string channelId)
    {
        throw new NotImplementedException();
    }
}