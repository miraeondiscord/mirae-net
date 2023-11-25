namespace MiraeNet.Discord;

/// <summary>
///     A set of properties used to configure the Discord services.
/// </summary>
public class DiscordOptions
{
    /// <summary>
    ///     The base URL for the Discord REST API.
    /// </summary>
    public string? ApiBaseUrl { get; set; }

    /// <summary>
    ///     The URL for the Discord Gateway server.
    /// </summary>
    public string? GatewayUrl { get; set; }

    /// <summary>
    ///     The token to use when authenticating with the Discord API.
    ///     If this is null, <see cref="Login" /> and <see cref="Password" />
    ///     cannot be null.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    ///     The login ID to use when authenticating with the Discord API.
    ///     If this is null, <see cref="Token" /> cannot be null.
    /// </summary>
    public string? Login { get; set; }

    /// <summary>
    ///     The password to use when authenticating with the Discord API.
    ///     If this is null, <see cref="Token" /> cannot be null.
    /// </summary>
    public string? Password { get; set; }
}