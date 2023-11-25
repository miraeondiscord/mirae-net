namespace MiraeNet.Core.Discord;

public interface ILifecycleManager
{
    Task StartAsync();
    Task StopAsync();
}