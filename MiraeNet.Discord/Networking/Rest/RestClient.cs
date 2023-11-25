using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Rest;

/// <summary>
///     Manages HTTP communication with the Discord REST API.
/// </summary>
public class RestClient
{
    /// <summary>
    ///     Instantiates a new instance of the <see cref="RestClient" /> class.
    /// </summary>
    /// <param name="options">The options to use for configuring the client.</param>
    /// <param name="logger">The logger to use for logging HTTP information.</param>
    public RestClient(DiscordOptions options, ILogger<RestClient> logger)
    {
        Http = new HttpClient(new RestDelegatingHandler(logger))
        {
            BaseAddress = new Uri(options.ApiBaseUrl!)
        };
    }

    /// <summary>
    ///     A specially configured <see cref="HttpClient" /> instance for making requests to the Discord REST API.
    /// </summary>
    public HttpClient Http { get; }

    /// <summary>
    ///     Sets the authentication token for subsequent requests.
    /// </summary>
    /// <param name="token">The authentication token to use.</param>
    public void SetToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            Http.DefaultRequestHeaders.Remove("Authorization");
            return;
        }

        Http.DefaultRequestHeaders.Add("Authorization", token);
    }

    /// <summary>
    ///     A DelegatingHandler that can be added to an <see cref="HttpClient" /> to add logging functionality.
    /// </summary>
    /// <param name="logger">The logger to use for logging request and response information.</param>
    private class RestDelegatingHandler(ILogger<RestClient> logger) : DelegatingHandler(new HttpClientHandler())
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var method = request.Method;
            var url = request.RequestUri;
            logger.LogInformation("-> REST {method}: {url}", method, url);
            var response = await base.SendAsync(request, cancellationToken);
            var status = response.StatusCode;
            logger.LogInformation("<- REST {status}: {url}", status, url);
            return response;
        }
    }
}