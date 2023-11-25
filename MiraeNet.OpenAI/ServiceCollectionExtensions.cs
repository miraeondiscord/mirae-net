using Microsoft.Extensions.DependencyInjection;
using MiraeNet.Core.Completion;

namespace MiraeNet.OpenAI;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the required services for the OpenAI API.
    /// </summary>
    /// <param name="services">The service collection to register services with.</param>
    /// <param name="options">The options that will be used for configuring the services.</param>
    public static void AddOpenAi(this IServiceCollection services, OpenAiOptions options)
    {
        // Register options object
        services.AddSingleton(options);

        // Register networking clients
        services.AddSingleton<OpenAiClient>();

        // Register services
        services.AddSingleton<ICompletionService, CompletionService>();
    }
}