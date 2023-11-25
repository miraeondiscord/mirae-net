namespace MiraeNet.Core.Discord;

// TODO: Full spec coverage

/// <summary>
///     Defines the contract for a service that manages Discord channels.
///     Please note that this is an incomplete definition.
/// </summary>
public interface IChannelService
{
    /// <summary>
    ///     Get a Discord channel by ID.
    /// </summary>
    /// <param name="channelId">
    ///     The ID of the channel to get.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> representing an asynchronous operation, which upon completion,
    ///     will contain the <see cref="Channel" /> object for the retrieved channel.
    /// </returns>
    Task<Channel> GetChannelAsync(string channelId);

    /// <summary>
    ///     Modifies a Discord channel by ID.
    /// </summary>
    /// <param name="channelId">
    ///     The ID of the channel to modify.
    /// </param>
    /// <param name="modifications">
    ///     A dictionary that represents the modifications to make on the channel, where each
    ///     key represents the JSON property of the channel to modify, and the value represents
    ///     the new value of the property. Refer to the
    ///     <a href="https://discord.com/developers/docs/resources/channel#modify-channel">
    ///         Discord Developer Documentation
    ///     </a>
    ///     for information on what JSON properties can be modified.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> representing an asynchronous operation, which upon completion,
    ///     will contain the <see cref="Channel" /> object for the modified channel.
    /// </returns>
    Task<Channel> ModifyChannelAsync(string channelId, Dictionary<string, dynamic> modifications);

    /// <summary>
    ///     Deletes a Discord channel by ID.
    /// </summary>
    /// <param name="channelId">
    ///     The ID of the channel to delete.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> representing an asynchronous operation, which upon completion,
    ///     will contain the <see cref="Channel" /> object for the deleted channel.
    /// </returns>
    Task<Channel> DeleteChannelAsync(string channelId);

    /// <summary>
    ///     Creates a new message in a Discord channel specified by ID.
    /// </summary>
    /// <param name="channelId">
    ///     The ID of the channel where the message will be created.
    /// </param>
    /// <param name="content">
    ///     The content of the message to be created.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation, which upon completion,
    ///     will contain the <see cref="Message" /> object for the created message.
    /// </returns>
    Task<Message> CreateMessageAsync(string channelId, string content);

    /// <summary>
    ///     Triggers the typing indicator in a Discord channel specified by ID.
    /// </summary>
    /// <param name="channelId">
    ///     The ID of the channel where the typing indicator will be triggered.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation.
    /// </returns>
    Task TriggerTypingIndicatorAsync(string channelId);
}