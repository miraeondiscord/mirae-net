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
    public required bool IsBot { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the user is an Official Discord System user.
    /// </summary>
    [JsonPropertyName("system")]
    public required bool IsSystem { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the user has two-factor authentication enabled.
    /// </summary>
    [JsonPropertyName("mfa_enabled")]
    public required bool HasMfaEnabled { get; set; }

    /// <summary>
    ///     Gets or sets the hash of the user's banner image.
    /// </summary>
    [JsonPropertyName("banner")]
    public string? Banner { get; set; }

    /// <summary>
    ///     Gets or sets the user's banner color encoded as an integer.
    /// </summary>
    [JsonPropertyName("accent_color")]
    public int? AccentColor { get; set; }

    /// <summary>
    ///     Gets or sets the user's chosen language option.
    /// </summary>
    [JsonPropertyName("locale")]
    public required string Locale { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the email on this account has been verified.
    /// </summary>
    [JsonPropertyName("verified")]
    public required bool IsVerified { get; set; }

    /// <summary>
    ///     Gets or sets the user's email address.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    ///     Gets or sets the flags on a user's account.
    /// </summary>
    [JsonPropertyName("flags")]
    public required int Flags { get; set; }

    /// <summary>
    ///     Gets or sets the type of Nitro subscription on a user's account.
    /// </summary>
    [JsonPropertyName("premium_type")]
    public required int PremiumType { get; set; }

    /// <summary>
    ///     Gets or sets the public flags on a user's account.
    /// </summary>
    [JsonPropertyName("public_flags")]
    public required int PublicFlags { get; set; }

    /// <summary>
    ///     Gets or sets the decoration of the user's avatar, if any.
    /// </summary>
    [JsonPropertyName("avatar_decoration")]
    public string? AvatarDecoration { get; set; }
}