using Microsoft.Extensions.Logging;

namespace MiraeNet.OpenAI;

/// <summary>
///     Manages HTTP communication with the OpenAI API.
/// </summary>
public class OpenAiClient
{
    /// <summary>
    ///     Instantiates a new instance of the <see cref="OpenAiClient" /> class.
    /// </summary>
    /// <param name="options">The options to use for configuring the client.</param>
    /// <param name="logger">The logger to use for logging HTTP information.</param>
    public OpenAiClient(OpenAiOptions options, ILogger<OpenAiClient> logger)
    {
        Http = new HttpClient(new RestDelegatingHandler(logger))
        {
            BaseAddress = new Uri(options.ApiBaseUrl!)
        };
        Http.DefaultRequestHeaders.Add("Authorization", "Bearer " + options.ApiKey!);
    }

    /// <summary>
    ///     A specially configured <see cref="HttpClient" /> instance for making requests to the OpenAI API.
    /// </summary>
    public HttpClient Http { get; }

    /// <summary>
    ///     A DelegatingHandler that can be added to an <see cref="HttpClient" /> to add logging functionality.
    /// </summary>
    /// <param name="logger">The logger to use for logging request and response information.</param>
    private class RestDelegatingHandler(ILogger logger) : DelegatingHandler(new HttpClientHandler())
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var method = request.Method;
            var url = request.RequestUri;
            logger.LogInformation("-> OPAI {method}: {url}", method, url);
            var response = await base.SendAsync(request, cancellationToken);
            var status = response.StatusCode;
            logger.LogInformation("<- OPAI {status}: {url}", status, url);
            return response;
        }
    }
}