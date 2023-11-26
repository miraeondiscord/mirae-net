using Microsoft.Extensions.DependencyInjection;

namespace MiraeNet.Core;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the core services.
    /// </summary>
    /// <param name="services">The service collection to register services with.</param>
    /// <param name="options">The options that will be used for configuring the services.</param>
    public static void AddCore(this IServiceCollection services, AgentOptions options)
    {
        services.AddSingleton<AgentOptions>();
        services.AddSingleton<Agent>();
    }
}