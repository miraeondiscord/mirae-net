using System.Text.Json.Serialization;

namespace MiraeNet.Core.Discord;

/// <summary>
///     Represents a Discord channel.
///     For more information, see the
///     <a href="https://discord.com/developers/docs/resources/channel">Discord Developer Documentation</a>.
/// </summary>
public class Channel
{
    /// <summary>
    ///     The unique identifier of the channel.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     The type of the channel.
    /// </summary>
    [JsonPropertyName("type")]
    public required ChannelType Type { get; set; }

    /// <summary>
    ///     The unique identifier of the guild the channel exists in.
    ///     This property is nullable as channels can exist outside of guilds. (e.g. Direct Messages)
    /// </summary>
    [JsonPropertyName("guild_id")]
    public string? GuildId { get; set; }

    /// <summary>
    ///     The display name of the channel.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     Whether the channel is marked NSFW (Not Safe for Work).
    /// </summary>
    [JsonPropertyName("nsfw")]
    public bool? IsNsfw { get; set; }
}

/// <summary>
///     Enumerates the types of Discord channels.
/// </summary>
public enum ChannelType
{
    /// <summary>
    ///     A text channel within a guild.
    /// </summary>
    GuildText = 0,

    /// <summary>
    ///     A direct text channel between users.
    /// </summary>
    DirectMessage = 1,

    /// <summary>
    ///     A voice channel within a guild.
    /// </summary>
    GuildVoice = 2,

    /// <summary>
    ///     A direct text channel between multiple users.
    /// </summary>
    GroupDirectMessage = 3,

    /// <summary>
    ///     An organizational category that contains up to 50 channels.
    /// </summary>
    GuildCategory = 4,

    /// <summary>
    ///     A channel that users can follow and crosspost into their
    ///     own guild (formerly news channels).
    /// </summary>
    GuildAnnouncement = 5,

    /// <summary>
    ///     A temporary sub-channel within a <see cref="GuildAnnouncement"/> channel.
    /// </summary>
    AnnouncementThread = 10,

    /// <summary>
    ///     A temporary sub-channel within a <see cref="GuildText"/> or <see cref="GuildForum"/> channel.
    /// </summary>
    PublicThread = 11,

    /// <summary>
    ///     A temporary sub-channel within a <see cref="GuildText"/> channel that is only
    ///     viewable by those invited and those with the MANAGE_THREADS permission.
    /// </summary>
    PrivateThread = 12,

    /// <summary>
    ///     A voice channel for hosting events with an audience.
    /// </summary>
    GuildStageVoice = 13,

    /// <summary>
    ///     The channel in a hub containing the listed guilds.
    /// </summary>
    GuildDirectory = 14,

    /// <summary>
    ///     A channel that can only contain threads.
    /// </summary>
    GuildForum = 15,

    /// <summary>
    ///     A channel that can only contain threads, similar to <see cref="GuildForum"/> channels.
    /// </summary>
    GuildMedia = 16
}