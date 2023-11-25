using Microsoft.Extensions.Logging;
using MiraeNet.Core.Discord;

namespace MiraeNet.Core;

public class Agent
{
    private readonly ILifecycleManager _discord;
    private readonly ILogger<Agent> _logger;

    public Agent(ILifecycleManager discord, ILogger<Agent> logger, IChannelService channelService,
        IEventService eventService)
    {
        _discord = discord;
        _logger = logger;
        eventService.MessageCreated += message =>
        {
            if (message.Author.Id == _discord.CurrentUser.Id)
                return;
            var channelId = message.ChannelId;
            var content = message.Content ?? "null";
            channelService.CreateMessageAsync(channelId, content);
        };
    }

    public Task StartAsync()
    {
        _logger.LogInformation("💖 Mirae 💖");
        _logger.LogInformation("Starting Agent.\n");
        return _discord.StartAsync();
    }

    public Task StopAsync()
    {
        _logger.LogInformation("Stopping Agent.");
        return _discord.StopAsync();
    }
}