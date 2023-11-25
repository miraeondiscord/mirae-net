using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using MiraeNet.Core.Completion;

namespace MiraeNet.OpenAI;

/// <summary>
///     A service that creates chat completions using
///     a the OpenAI API.
/// </summary>
public class CompletionService
    (OpenAiClient client, OpenAiOptions options, ILogger<CompletionService> logger) : ICompletionService
{
    public async Task<CompletionMessage> CreateCompletionAsync(IList<CompletionMessage> messages)
    {
        logger.LogInformation("Generating chat completion. - Conversation Length: {length}", messages.Count);
        var requestData = new Dictionary<string, dynamic>
        {
            { "model", options.Model! },
            { "messages", messages }
        };
        var response = await client.Http.PostAsJsonAsync("chat/completions", requestData);
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<Dictionary<string, JsonElement>>();
        var tokenCount = responseData!["usage"].GetProperty("total_tokens").GetInt32();
        var generatedMessage = responseData["choices"]
            .EnumerateArray()
            .First()
            .GetProperty("message")
            .Deserialize<CompletionMessage>();
        logger.LogInformation("Total Tokens Used: {total}", tokenCount);
        return generatedMessage!;
    }
}