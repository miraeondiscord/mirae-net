using Microsoft.Extensions.Logging;
using MiraeNet.Core.Discord;
using MiraeNet.Discord.Networking.Gateway;
using MiraeNet.Discord.Networking.Rest;

namespace MiraeNet.Discord;

public class LifecycleManager(
    DiscordOptions options,
    RestClient rest,
    GatewayClient gateway,
    AuthService auth,
    IUserService user,
    ILogger<LifecycleManager> logger) : ILifecycleManager
{
    public User CurrentUser
    {
        get
        {
            if (_user is null)
                throw new InvalidOperationException(
                    "Attempted to access CurrentUser when Discord services has not fully been started.");
            return _user;
        }
    }

    private User? _user;

    public async Task StartAsync()
    {
        logger.LogInformation("Starting Discord services.");

        // Get token from options, which might be a null/empty string.
        var token = options.Token;
        if (string.IsNullOrEmpty(token))
            // If the token provided from the options is null/empty,
            // login with the credentials provided from the options
            // and get the token that way.
            token = await auth.LoginAsync(options.Login!, options.Password!);

        // Configure the REST client to use the token for all
        // subsequent requests.
        rest.SetToken(token);

        // Start the gateway connection and identify the client to the server.
        await gateway.StartAsync();
        await gateway.Identifier.IdentifyAsync(token);

        // Get our user's information
        _user = await user.GetCurrentUserAsync();
    }

    public Task StopAsync()
    {
        // Simply stop the gateway connection and no more.
        logger.LogInformation("Stopping Discord services.");
        return gateway.StopAsync();
    }
}