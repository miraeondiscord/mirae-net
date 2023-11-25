namespace MiraeNet.Core.Discord;

/// <summary>
///     Defines the API for a managing class that manages the lifecycle and global state of Discord services.
/// </summary>
public interface ILifecycleManager
{
    /// <summary>
    ///     The currently logged in User.
    /// </summary>
    User CurrentUser { get; }

    /// <summary>
    ///     Start all the Discord services and login to Discord.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation.
    /// </returns>
    Task StartAsync();

    /// <summary>
    ///     Stop all the Discord services.
    ///     This operation is not guaranteed to be reversible.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation.
    /// </returns>
    Task StopAsync();
}