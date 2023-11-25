using MiraeNet.Core.Discord;

namespace MiraeNet.Discord;

/// <summary>
///     A service that manages Discord users.
///     Please note that this is an incomplete definition.
/// </summary>
public class UserService : IUserService
{
    public Task<User> GetMyUserAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(string userId)
    {
        throw new NotImplementedException();
    }
}