using System.Text.Json.Serialization;

namespace MiraeNet.Core.Discord;

/// <summary>
///     Represents a Discord user.
///     For more information, see the
///     <a href="https://discord.com/developers/docs/resources/user">Discord Developer Documentation</a>.
/// </summary>
public class User
{
    /// <summary>
    ///     The unique identifier for the user.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     The username of the user.
    /// </summary>
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    /// <summary>
    ///     The discriminator tag of the user.
    /// </summary>
    [JsonPropertyName("discriminator")]
    public required string Discriminator { get; set; }

    /// <summary>
    ///     The display name of the user, if set. For bots, this represents the application name.
    /// </summary>
    [JsonPropertyName("global_name")]
    public string? GlobalName { get; set; }

    /// <summary>
    ///     The hash of the user's avatar.
    /// </summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    /// <summary>
    ///     A value indicating whether the user is associated with an OAuth2 application.
    /// </summary>
    [JsonPropertyName("bot")]
    public bool? IsBot { get; set; }
}