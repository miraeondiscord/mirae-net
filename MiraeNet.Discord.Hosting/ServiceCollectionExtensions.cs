using Microsoft.Extensions.DependencyInjection;
using MiraeNet.Core.Discord;
using MiraeNet.Discord.Gateway;
using MiraeNet.Discord.Rest;

namespace MiraeNet.Discord.Hosting;

public static class ServiceCollectionExtensions
{
    public static void AddDiscordServices(this IServiceCollection services, DiscordOptions options)
    {
        services.AddSingleton(options); // TODO: Is this right?
        services.AddSingleton<IDiscordContext, DiscordContext>();
        services.AddSingleton<RestClient>();
        services.AddSingleton<GatewayClient>();
        services.AddSingleton<AuthService>();
        services.AddSingleton<IUserService, UserService>();
    }
}