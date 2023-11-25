using Microsoft.Extensions.DependencyInjection;
using MiraeNet.Core.Discord;
using MiraeNet.Discord.Networking.Gateway;
using MiraeNet.Discord.Networking.Rest;

namespace MiraeNet.Discord;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the required services for the standard implementation of the Discord API.
    /// </summary>
    /// <param name="services">The service collection to register services with.</param>
    /// <param name="options">The options that will be used for configuring the services.</param>
    public static void AddDiscord(this IServiceCollection services, DiscordOptions options)
    {
        // Register options object
        services.AddSingleton(options);

        // Register lifecycle manager (Start/Stop)
        services.AddSingleton<ILifecycleManager, LifecycleManager>();

        // Register networking clients
        services.AddSingleton<RestClient>();
        services.AddSingleton<GatewayClient>();

        // Register services
        services.AddSingleton<IEventService, EventService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IChannelService, ChannelService>();
        services.AddSingleton<AuthService>();
    }
}