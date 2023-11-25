namespace MiraeNet.Core.Discord;

public interface ILifecycleManager
{
    User CurrentUser { get; }
    Task StartAsync();
    Task StopAsync();
}