using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using MiraeNet.Core.Discord;
using MiraeNet.Discord.Networking.Rest;

namespace MiraeNet.Discord;

/// <summary>
///     A service that manages Discord channels.
///     For more information, see the
///     <a href="https://discord.com/developers/docs/resources/channel">Discord Developer Documentation</a>.
/// </summary>
public class ChannelService(RestClient rest, ILogger<ChannelService> logger) : IChannelService
{
    public async Task<Message> CreateMessageAsync(string channelId, string content)
    {
        logger.LogInformation("Creating new message. - Channel: {id}", channelId);
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