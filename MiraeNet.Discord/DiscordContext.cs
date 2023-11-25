using MiraeNet.Core.Discord;
using MiraeNet.Discord.Gateway;

namespace MiraeNet.Discord;

/// <summary>
///     Represents the context in which Discord-related services are running in.
/// </summary>
public class DiscordContext
    (AuthService auth, GatewayClient gateway, DiscordOptions options) : IDiscordContext
{
    private User? _user;

    /// <summary>
    ///     The authentication token for the current login session.
    /// </summary>
    public string Token { get; internal set; } = options.Token ?? string.Empty;

    public User MyUser
    {
        get
        {
            if (_user is null)
                throw new InvalidOperationException("Tried to get my user when the client has not logged in yet.");
            return _user!;
        }
        internal set => _user = value;
    }

    public async Task StartAsync()
    {
        if (string.IsNullOrEmpty(Token))
            Token = await auth.LoginAsync(options.Login!, options.Password!);
        await gateway.StartAsync();
        throw new NotImplementedException();
    }

    public Task StopAsync()
    {
        return gateway.StopAsync();
    }
}