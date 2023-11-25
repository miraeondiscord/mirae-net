using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiraeNet.Discord.Networking.Gateway;

public class IncomingPayload
{
    [JsonPropertyName("op")] public required int OpCode { get; set; }
    [JsonPropertyName("t")] public string? EventName { get; set; }
    [JsonPropertyName("s")] public int? SequenceIndex { get; set; }
    [JsonPropertyName("d")] public JsonElement? Data { get; set; }

    public TData GetData<TData>()
    {
        return Data!.Value.Deserialize<TData>()!;
    }
}