using System.Text.Json.Serialization;

namespace MiraeNet.Core.Discord;

// TODO: Full spec coverage
// TODO: GPT proofread

/// <summary>
///     Represents a Discord message object.
///     For more information, see
///     <a href="https://discord.com/developers/docs/resources/channel">Discord Developer Documentation</a>.
///     Please note that this is an incomplete implementation.
/// </summary>
public class Message
{
    /// <summary>
    ///     Gets or sets the unique identifier for the message.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the channel the message was sent in.
    /// </summary>
    [JsonPropertyName("channel_id")]
    public required string ChannelId { get; set; }

    /// <summary>
    ///     Gets or sets the author of the message. This is not guaranteed to be a valid user
    ///     (e.g., a webhook could be the author).
    /// </summary>
    [JsonPropertyName("author")]
    public required User Author { get; set; }

    /// <summary>
    ///     Gets or sets the content of the message.
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>
    ///     Gets or sets the timestamp when the message was sent.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    ///     Gets or sets the timestamp when the message was last edited.
    ///     Null if the message has never been edited.
    /// </summary>
    [JsonPropertyName("edited_timestamp")]
    public DateTimeOffset? EditedTimestamp { get; set; }

    /// <summary>
    ///     Gets or sets whether the message is a Text-To-Speech message.
    /// </summary>
    [JsonPropertyName("tts")]
    public bool Tts { get; set; }

    /// <summary>
    ///     Gets or sets whether the message mentions everyone.
    /// </summary>
    [JsonPropertyName("mention_everyone")]
    public bool MentionEveryone { get; set; }

    /// <summary>
    ///     Gets or sets the collection of user mentions in the message.
    /// </summary>
    [JsonPropertyName("mentions")]
    public required List<User> Mentions { get; set; }

    /// <summary>
    ///     Gets or sets the collection of role object ids mentioned in the message.
    /// </summary>
    [JsonPropertyName("mention_roles")]
    public required List<string> MentionRoles { get; set; }

    /// <summary>
    ///     Gets or sets the collection of channel mentions in the message.
    /// </summary>
    [JsonPropertyName("mention_channels")]
    public required List<ChannelMention> MentionChannels { get; set; }

    /// <summary>
    ///     Gets or sets the collection of attachments in the message.
    /// </summary>
    [JsonPropertyName("attachments")]
    public required List<Attachment> Attachments { get; set; }

    /// <summary>
    ///     Gets or sets a nonce that can be used for optimistic message sending.
    /// </summary>
    [JsonPropertyName("nonce")]
    public string? Nonce { get; set; }

    /// <summary>
    ///     Gets or sets whether the message is pinned.
    /// </summary>
    [JsonPropertyName("pinned")]
    public bool Pinned { get; set; }

    /// <summary>
    ///     Gets or sets the webhook ID if the message was generated by a webhook.
    /// </summary>
    [JsonPropertyName("webhook_id")]
    public string? WebhookId { get; set; }

    /// <summary>
    ///     Gets or sets the type of message.
    /// </summary>
    [JsonPropertyName("type")]
    public MessageType Type { get; set; }

    /// <summary>
    ///     Gets or sets the application ID if the message is an interaction or
    ///     application-owned webhook.
    /// </summary>
    [JsonPropertyName("application_id")]
    public string? ApplicationId { get; set; }

    /// <summary>
    ///     Gets or sets the message reference object data showing the source of a crosspost,
    ///     channel follow add, pin, or reply message.
    /// </summary>
    [JsonPropertyName("message_reference")]
    public MessageReference? MessageReference { get; set; }

    /// <summary>
    ///     Gets or sets the message flags combined as a bitfield.
    /// </summary>
    [JsonPropertyName("flags")]
    public int? Flags { get; set; }

    /// <summary>
    ///     Gets or sets the message associated with the message_reference.
    /// </summary>
    [JsonPropertyName("referenced_message")]
    public Message? ReferencedMessage { get; set; }

    /// <summary>
    ///     Gets or sets the channel object for the thread that was started
    ///     from this message.
    /// </summary>
    [JsonPropertyName("thread")]
    public Channel? Thread { get; set; }

    /// <summary>
    ///     Gets or sets the integer representing the approximate position of the message in a thread.
    /// </summary>
    [JsonPropertyName("position")]
    public int? Position { get; set; }
}

/// <summary>
///     Represents the type of message.
/// </summary>
public enum MessageType
{
    /// <summary>
    ///     The default message type.
    /// </summary>
    Default = 0,

    /// <summary>
    ///     A message indicating a recipient was added.
    /// </summary>
    RecipientAdd = 1,

    /// <summary>
    ///     A message indicating a recipient was removed.
    /// </summary>
    RecipientRemove = 2,

    /// <summary>
    ///     A message indicating a call.
    /// </summary>
    Call = 3,

    /// <summary>
    ///     A message indicating a channel name change.
    /// </summary>
    ChannelNameChange = 4,

    /// <summary>
    ///     A message indicating a channel icon change.
    /// </summary>
    ChannelIconChange = 5,

    /// <summary>
    ///     A message indicating a channel pinned message.
    /// </summary>
    ChannelPinnedMessage = 6,

    /// <summary>
    ///     A message indicating a user has joined.
    /// </summary>
    UserJoin = 7,

    /// <summary>
    ///     A message indicating a guild boost.
    /// </summary>
    GuildBoost = 8,

    /// <summary>
    ///     A message indicating a guild boost to tier 1.
    /// </summary>
    GuildBoostTier1 = 9,

    /// <summary>
    ///     A message indicating a guild boost to tier 2.
    /// </summary>
    GuildBoostTier2 = 10,

    /// <summary>
    ///     A message indicating a guild boost to tier 3.
    /// </summary>
    GuildBoostTier3 = 11,

    /// <summary>
    ///     A message indicating a channel follow add.
    /// </summary>
    ChannelFollowAdd = 12,

    /// <summary>
    ///     A message indicating guild discovery disqualification.
    /// </summary>
    GuildDiscoveryDisqualified = 14,

    /// <summary>
    ///     A message indicating guild discovery requalification.
    /// </summary>
    GuildDiscoveryRequalified = 15,

    /// <summary>
    ///     A message indicating an initial warning for guild discovery grace period.
    /// </summary>
    GuildDiscoveryGracePeriodInitialWarning = 16,

    /// <summary>
    ///     A message indicating a final warning for guild discovery grace period.
    /// </summary>
    GuildDiscoveryGracePeriodFinalWarning = 17,

    /// <summary>
    ///     A message indicating a thread was created.
    /// </summary>
    ThreadCreated = 18,

    /// <summary>
    ///     A message indicating a reply to another message.
    /// </summary>
    Reply = 19,

    /// <summary>
    ///     A message generated by a chat input command.
    /// </summary>
    ChatInputCommand = 20,

    /// <summary>
    ///     A message that is the first in a thread.
    /// </summary>
    ThreadStarterMessage = 21,

    /// <summary>
    ///     A message that reminds about a guild invite.
    /// </summary>
    GuildInviteReminder = 22,

    /// <summary>
    ///     A message generated by a context menu command.
    /// </summary>
    ContextMenuCommand = 23,

    /// <summary>
    ///     A message generated by an auto moderation action.
    /// </summary>
    AutoModerationAction = 24,

    /// <summary>
    ///     A message related to role subscription purchase.
    /// </summary>
    RoleSubscriptionPurchase = 25,

    /// <summary>
    ///     A message indicating an interaction for premium upsell.
    /// </summary>
    InteractionPremiumUpsell = 26,

    /// <summary>
    ///     A message indicating the start of a stage.
    /// </summary>
    StageStart = 27,

    /// <summary>
    ///     A message indicating the end of a stage.
    /// </summary>
    StageEnd = 28,

    /// <summary>
    ///     A message indicating a stage speaker.
    /// </summary>
    StageSpeaker = 29,

    // Note: Type 30 is missing in the provided list.

    /// <summary>
    ///     A message indicating a stage topic.
    /// </summary>
    StageTopic = 31,

    /// <summary>
    ///     A message related to guild application premium subscription.
    /// </summary>
    GuildApplicationPremiumSubscription = 32
}

/// <summary>
///     Represents a mention or ping in a channel.
/// </summary>
public class ChannelMention
{
    /// <summary>
    ///     Gets or sets the unique identifier for the mentioned channel.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier for the guild containing the channel.
    /// </summary>
    [JsonPropertyName("guild_id")]
    public required string GuildId { get; set; }

    /// <summary>
    ///     Gets or sets the type of mentioned channel.
    /// </summary>
    [JsonPropertyName("type")]
    public required ChannelType Type { get; set; }

    /// <summary>
    ///     Gets or sets the name of the mentioned channel.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

/// <summary>
///     Represents an attachment in a Discord message.
/// </summary>
public class Attachment
{
    /// <summary>
    ///     Gets or sets the unique identifier for the attachment.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the file attached.
    /// </summary>
    [JsonPropertyName("filename")]
    public required string Filename { get; set; }

    /// <summary>
    ///     Gets or sets the description for the file.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    ///     Gets or sets the media type of the attachment.
    /// </summary>
    [JsonPropertyName("content_type")]
    public string? ContentType { get; set; }

    /// <summary>
    ///     Gets or sets the size of the file in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public required int Size { get; set; }

    /// <summary>
    ///     Gets or sets the source URL of the file.
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    /// <summary>
    ///     Gets or sets a proxied URL of the file.
    /// </summary>
    [JsonPropertyName("proxy_url")]
    public required string ProxyUrl { get; set; }

    /// <summary>
    ///     Gets or sets the height of the file if it is an image.
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; set; }

    /// <summary>
    ///     Gets or sets the width of the file if it is an image.
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this attachment is ephemeral.
    /// </summary>
    [JsonPropertyName("ephemeral")]
    public bool? IsEphemeral { get; set; }

    /// <summary>
    ///     Gets or sets the duration of the audio file, if applicable (currently for voice messages).
    /// </summary>
    [JsonPropertyName("duration_secs")]
    public float? DurationSecs { get; set; }

    /// <summary>
    ///     Gets or sets the base64 encoded bytearray representing a sampled waveform (currently for voice messages).
    /// </summary>
    [JsonPropertyName("waveform")]
    public string? Waveform { get; set; }

    /// <summary>
    ///     Gets or sets the attachment flags combined as a bitfield.
    /// </summary>
    [JsonPropertyName("flags")]
    public int? Flags { get; set; }
}

/// <summary>
///     Represents a user's "reply" to a specific message in a channel.
/// </summary>
public class MessageReference
{
    /// <summary>
    ///     Gets or sets the unique identifier of the originating message.
    /// </summary>
    [JsonPropertyName("message_id")]
    public string? MessageId { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier of the originating message's channel.
    /// </summary>
    [JsonPropertyName("channel_id")]
    public string? ChannelId { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier of the originating message's guild.
    /// </summary>
    [JsonPropertyName("guild_id")]
    public string? GuildId { get; set; }

    /// <summary>
    ///     When sending, whether to error if the referenced message doesn't exist
    ///     instead of sending as a normal (non-reply) message.
    /// </summary>
    [JsonPropertyName("fail_if_not_exists")]
    public bool? FailIfNotExists { get; set; }
}