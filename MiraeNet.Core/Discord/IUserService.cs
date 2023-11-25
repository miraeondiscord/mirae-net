namespace MiraeNet.Core.Discord;

// TODO: Full spec coverage

/// <summary>
///     Defines the contract for a service that manages Discord users.
///     Please note that this is an incomplete definition.
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Get the currently logged in Discord user.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task" /> representing an asynchronous operation, which upon completion,
    ///     will contain the <see cref="User" /> object for the retrieved user.
    /// </returns>
    Task<User> GetMyUserAsync();

    /// <summary>
    ///     Get a Discord user by ID.
    /// </summary>
    /// <param name="userId">
    ///     The ID of the Discord user to get.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> representing an asynchronous operation, which upon completion,
    ///     will contain the <see cref="User" /> object for the retrieved user.
    /// </returns>
    Task<User> GetUserAsync(string userId);
}