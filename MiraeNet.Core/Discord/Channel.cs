using System.Text.Json.Serialization;

namespace MiraeNet.Core.Discord;

// TODO: Full spec coverage
// TODO: GPT proofread

/// <summary>
///     Represents a Discord channel object.
///     For more information, see
///     <a href="https://discord.com/developers/docs/resources/channel">Discord Developer Documentation</a>.
///     Please note that this is an incomplete implementation.
/// </summary>
public class Channel
{
    /// <summary>
    ///     Gets or sets the ID of this channel.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     Gets or sets the type of channel.
    /// </summary>
    [JsonPropertyName("type")]
    public required ChannelType Type { get; set; }

    /// <summary>
    ///     Gets or sets the ID of the guild.
    ///     May be missing for some channel objects received over gateway guild dispatches.
    /// </summary>
    [JsonPropertyName("guild_id")]
    public string? GuildId { get; set; }

    /// <summary>
    ///     Gets or sets the sorting position of the channel.
    /// </summary>
    [JsonPropertyName("position")]
    public int? Position { get; set; }

    /// <summary>
    ///     Gets or sets explicit permission overwrites for members and roles.
    /// </summary>
    [JsonPropertyName("permission_overwrites")]
    public List<PermissionOverwrite>? PermissionOverwrites { get; set; }

    /// <summary>
    ///     Gets or sets the name of the channel (1-100 characters).
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the channel topic (0-1024 characters,
    ///     except for GUILD_FORUM and GUILD_MEDIA channels which can be 0-4096 characters).
    /// </summary>
    [JsonPropertyName("topic")]
    public string? Topic { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the channel is NSFW.
    /// </summary>
    [JsonPropertyName("nsfw")]
    public bool? IsNsfw { get; set; }

    /// <summary>
    ///     Gets or sets the ID of the last message sent in this channel.
    ///     May not point to an existing or valid message.
    /// </summary>
    [JsonPropertyName("last_message_id")]
    public string? LastMessageId { get; set; }

    /// <summary>
    ///     Gets or sets the bitrate (in bits) of the voice channel.
    /// </summary>
    [JsonPropertyName("bitrate")]
    public int? Bitrate { get; set; }

    /// <summary>
    ///     Gets or sets the user limit of the voice channel.
    /// </summary>
    [JsonPropertyName("user_limit")]
    public int? UserLimit { get; set; }

    /// <summary>
    ///     Gets or sets the rate limit per user in seconds.
    /// </summary>
    [JsonPropertyName("rate_limit_per_user")]
    public int? RateLimitPerUser { get; set; }

    /// <summary>
    ///     Gets or sets the recipients of the DM.
    /// </summary>
    [JsonPropertyName("recipients")]
    public List<User>? Recipients { get; set; }

    /// <summary>
    ///     Gets or sets the icon hash of the group DM.
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    /// <summary>
    ///     Gets or sets the ID of the creator of the group DM or thread.
    /// </summary>
    [JsonPropertyName("owner_id")]
    public string? OwnerId { get; set; }

    /// <summary>
    ///     Gets or sets the application ID of the group DM creator if it is bot-created.
    /// </summary>
    [JsonPropertyName("application_id")]
    public string? ApplicationId { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the channel is managed by an application.
    /// </summary>
    [JsonPropertyName("managed")]
    public bool? IsManaged { get; set; }

    /// <summary>
    ///     Gets or sets the ID of the parent category for a channel,
    ///     or the ID of the text channel this thread was created.
    /// </summary>
    [JsonPropertyName("parent_id")]
    public string? ParentId { get; set; }

    /// <summary>
    ///     Gets or sets the timestamp of when the last pinned message was pinned.
    /// </summary>
    [JsonPropertyName("last_pin_timestamp")]
    public DateTimeOffset? LastPinTimestamp { get; set; }
}

/// <summary>
///     Specifies the types of Discord channels.
/// </summary>
public enum ChannelType
{
    /// <summary>
    ///     A text channel within a server.
    /// </summary>
    GuildText = 0,

    /// <summary>
    ///     A direct message between users.
    /// </summary>
    DirectMessage = 1,

    /// <summary>
    ///     A voice channel within a server.
    /// </summary>
    GuildVoice = 2,

    /// <summary>
    ///     A direct message between multiple users.
    /// </summary>
    GroupDirectMessage = 3,

    /// <summary>
    ///     An organizational category that contains up to 50 channels.
    /// </summary>
    GuildCategory = 4,

    /// <summary>
    ///     A channel that users can follow and crosspost into their
    ///     own server (formerly news channels).
    /// </summary>
    GuildAnnouncement = 5,

    /// <summary>
    ///     A temporary sub-channel within a GUILD_ANNOUNCEMENT channel.
    /// </summary>
    AnnouncementThread = 10,

    /// <summary>
    ///     A temporary sub-channel within a GUILD_TEXT or GUILD_FORUM channel.
    /// </summary>
    PublicThread = 11,

    /// <summary>
    ///     A temporary sub-channel within a GUILD_TEXT channel that is only
    ///     viewable by those invited and those with the MANAGE_THREADS permission.
    /// </summary>
    PrivateThread = 12,

    /// <summary>
    ///     A voice channel for hosting events with an audience.
    /// </summary>
    GuildStageVoice = 13,

    /// <summary>
    ///     The channel in a hub containing the listed servers.
    /// </summary>
    GuildDirectory = 14,

    /// <summary>
    ///     A channel that can only contain threads.
    /// </summary>
    GuildForum = 15,

    /// <summary>
    ///     A channel that can only contain threads, similar to GUILD_FORUM channels.
    /// </summary>
    GuildMedia = 16
}

/// <summary>
///     Represents a permission overwrite for a role or user in a Discord channel.
/// </summary>
public class PermissionOverwrite
{
    /// <summary>
    ///     Gets or sets the ID of the role or user.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     Gets or sets the type, indicating whether this is a role (0) or a member (1).
    /// </summary>
    [JsonPropertyName("type")]
    public required int Type { get; set; }

    /// <summary>
    ///     Gets or sets the allowed permission bit set.
    /// </summary>
    [JsonPropertyName("allow")]
    public required string Allow { get; set; }

    /// <summary>
    ///     Gets or sets the denied permission bit set.
    /// </summary>
    [JsonPropertyName("deny")]
    public required string Deny { get; set; }
}