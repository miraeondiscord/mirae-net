namespace MiraeNet.Discord;

// TODO: Document

/// <summary>
///     A set of properties used to configure the Discord service implementations.
/// </summary>
public class DiscordOptions
{
    public string? ApiBaseUrl { get; set; }
    public string? GatewayUrl { get; set; }
    public string? Token { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
}