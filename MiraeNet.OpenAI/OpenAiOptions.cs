namespace MiraeNet.OpenAI;

/// <summary>
///     A set of properties used to configure the OpenAI services.
/// </summary>
public class OpenAiOptions
{
    /// <summary>
    ///     The base URL for the OpenAI API.
    /// </summary>
    public string? ApiBaseUrl { get; set; }

    /// <summary>
    ///     The API key to use for authenticating with the API.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    ///     The model to use for generating completions.
    /// </summary>
    public string? Model { get; set; }
}