using Microsoft.Extensions.DependencyInjection;

namespace MiraeNet.Core;

public static class ServiceCollectionExtensions
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddSingleton<Agent>();
    }
}