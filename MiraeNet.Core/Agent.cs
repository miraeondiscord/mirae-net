using Microsoft.Extensions.Logging;
using MiraeNet.Core.Discord;

namespace MiraeNet.Core;

public class Agent(ILifecycleManager discord, ILogger<Agent> logger)
{
    public Task StartAsync()
    {
        logger.LogInformation("💖 Mirae 💖");
        logger.LogInformation("Starting Agent.\n");
        return discord.StartAsync();
    }

    public Task StopAsync()
    {
        logger.LogInformation("Stopping Agent.");
        return discord.StopAsync();
    }
}