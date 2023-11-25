using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using MiraeNet.Core.Discord;
using MiraeNet.Discord.Networking.Rest;

namespace MiraeNet.Discord;

/// <summary>
///     A service that manages Discord users.
///     For more information, see the
///     <a href="https://discord.com/developers/docs/resources/user">Discord Developer Documentation</a>.
/// </summary>
public class UserService(RestClient rest, ILogger<UserService> logger) : IUserService
{
    public async Task<User> GetCurrentUserAsync()
    {
        logger.LogInformation("Getting current user info.");
        var response = await rest.Http.GetAsync("users/@me");
        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<User>();
        return user!;
    }

    public Task<User> GetUserAsync(string userId)
    {
        throw new NotImplementedException();
    }
}