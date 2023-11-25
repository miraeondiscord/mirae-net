namespace MiraeNet.Core.Discord;

/// <summary>
///     Defines the API for a service that manages Discord users.
///     For more information, see the
///     <a href="https://discord.com/developers/docs/resources/user">Discord Developer Documentation</a>.
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Get the currently logged in user.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation, which upon completion,
    ///     will contain the <see cref="User" /> object for the retrieved user.
    /// </returns>
    Task<User> GetCurrentUserAsync();

    /// <summary>
    ///     Get a user by ID.
    /// </summary>
    /// <param name="userId">
    ///     The ID of the user to get.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation, which upon completion,
    ///     will contain the <see cref="User" /> object for the retrieved user.
    /// </returns>
    Task<User> GetUserAsync(string userId);
}