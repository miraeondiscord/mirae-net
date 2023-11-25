using Microsoft.Extensions.Logging;
using MiraeNet.Core.Completion;
using MiraeNet.Core.Discord;

namespace MiraeNet.Core;

public class Agent
{
    private readonly ILifecycleManager _discord;
    private readonly ILogger<Agent> _logger;

    public Agent(ILifecycleManager discord, ILogger<Agent> logger, IChannelService channelService,
        IEventService eventService, ICompletionService completionService)
    {
        _discord = discord;
        _logger = logger;
    }

    public Task StartAsync()
    {
        _logger.LogInformation("💖 Mirae 💖");
        _logger.LogInformation("Starting Agent.\n");
        return _discord.StartAsync();
    }

    public Task StopAsync()
    {
        _logger.LogInformation("Stopping Agent.\n");
        return _discord.StopAsync();
    }
}