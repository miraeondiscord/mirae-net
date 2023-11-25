using System.Text.Json.Serialization;

namespace MiraeNet.Discord.Gateway;

internal class OutgoingPayload<TData>
{
    [JsonPropertyName("op")] public required int OpCode { get; set; }
    [JsonPropertyName("d")] public required TData Data { get; set; }
}