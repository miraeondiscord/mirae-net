namespace MiraeNet.Core.Discord;

/// <summary>
///     Represents the context in which Discord-related services are running in.
/// </summary>
public interface IDiscordContext
{
    /// <summary>
    ///     The <see cref="User" /> instance for the currently logged in user.
    /// </summary>
    User MyUser { get; }

    /// <summary>
    ///     Setup the necessary network connections and login to Discord.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation.
    /// </returns>
    Task StartAsync();

    /// <summary>
    ///     Stop all network connections to Discord. This method is not guaranteed
    ///     to be reversible.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation.
    /// </returns>
    Task StopAsync();
}