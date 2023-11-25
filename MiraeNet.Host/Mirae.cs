using Microsoft.Extensions.Hosting;
using MiraeNet.Core.Discord;

namespace MiraeNet.Host;

public class Mirae(IDiscordContext discord) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return discord.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return discord.StopAsync();
    }
}