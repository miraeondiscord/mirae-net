using System.Text.Json.Serialization;

namespace MiraeNet.Core.Discord;

// TODO: Full spec coverage
// TODO: GPT proofread

/// <summary>
///     Represents a Discord user object.
///     For more information, see
///     <a href="https://discord.com/developers/docs/resources/user">Discord Developer Documentation</a>.
/// </summary>
public class User
{
    /// <summary>
    ///     Gets or sets the user's ID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     Gets or sets the user's username.
    /// </summary>
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    /// <summary>
    ///     Gets or sets the user's discriminator tag.
    /// </summary>
    [JsonPropertyName("discriminator")]
    public required string Discriminator { get; set; }

    /// <summary>
    ///     Gets or sets the user's display name, if set. For bots, this is the application name.
    /// </summary>
    [JsonPropertyName("global_name")]
    public string? GlobalName { get; set; }

    /// <summary>
    ///     Gets or sets the hash of the user's avatar.
    /// </summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the user belongs to an OAuth2 application.
    /// </summary>
    [JsonPropertyName("bot")]
    public bool? IsBot { get; set; }
}