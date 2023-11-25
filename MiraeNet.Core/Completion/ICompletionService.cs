namespace MiraeNet.Core.Completion;

/// <summary>
///     Defines the contract for service that creates chat completions using
///     a Large Language Model (LLM).
/// </summary>
public interface ICompletionService
{
    /// <summary>
    ///     Creates a completion message based on the provided list of conversation messages.
    /// </summary>
    /// <param name="messages">
    ///     A list of <see cref="CompletionMessage" /> instances representing the conversation so far.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation, which upon completion,
    ///     returns a <see cref="CompletionMessage" /> representing the generated completion.
    /// </returns>
    Task<CompletionMessage> CreateCompletionAsync(IList<CompletionMessage> messages);
}