using Microsoft.Extensions.Hosting;
using MiraeNet.Core;

namespace MiraeNet.Host;

public class Service(Agent agent) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return agent.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return agent.StopAsync();
    }
}