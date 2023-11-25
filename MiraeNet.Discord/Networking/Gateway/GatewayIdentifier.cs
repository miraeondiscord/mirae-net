using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Gateway;

public class GatewayIdentifier(GatewayClient client)
{
    public async Task IdentifyAsync(string token)
    {
        client.Logger.LogInformation("Sending identification to gateway.");
        var data = IdentifyPayloadData.Forge();
        data.Token = token;
        var payload = new OutgoingPayload<IdentifyPayloadData>
        {
            OpCode = 2,
            Data = data
        };
        await client.SendPayloadAsync(payload);
    }
}

internal class IdentifyPayloadData
{
    [JsonPropertyName("token")] public required string Token { get; set; }
    [JsonPropertyName("capabilities")] public required int Capabilities { get; set; }
    [JsonPropertyName("properties")] public required IdentifyEventProperties Properties { get; set; }
    [JsonPropertyName("presence")] public required IdentifyEventPresence Presence { get; set; }
    [JsonPropertyName("compress")] public required bool Compress { get; set; }
    [JsonPropertyName("client_state")] public required IdentifyEventClientState ClientState { get; set; }

    public static IdentifyPayloadData Forge()
    {
        return new IdentifyPayloadData
        {
            Token = string.Empty,
            Capabilities = 16381,
            Properties = new IdentifyEventProperties
            {
                OperatingSystem = "Mac OS X",
                Browser = "Safari",
                Device = string.Empty,
                SystemLocale = "en-US",
                BrowserUserAgent =
                    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.1 Safari/605.1.15",
                BrowserVersion = "17.1",
                OperatingSystemVersion = "10.15.7",
                Referrer = string.Empty,
                ReferringDomain = string.Empty,
                ReferringDomainCurrent = string.Empty,
                ReleaseChannel = "stable",
                ClientBuildNumber = 24732,
                ClientEventSource = null
            },
            Presence = new IdentifyEventPresence
            {
                Status = "online",
                Since = 0,
                Activites = new List<string>(),
                AwayFromKeyboard = true
            },
            Compress = false,
            ClientState = new IdentifyEventClientState
            {
                GuildVersions = new Dictionary<string, string>(),
                HighestLastMessageId = "0",
                ReadStateVersion = 0,
                UserGuildSettingsVersion = -1,
                UserSettingsVersion = -1,
                PrivateChannelsVersion = "0",
                ApiCodeVersion = 0
            }
        };
    }
}

internal struct IdentifyEventProperties
{
    [JsonPropertyName("os")] public required string OperatingSystem { get; set; }
    [JsonPropertyName("browser")] public required string Browser { get; set; }
    [JsonPropertyName("device")] public required string Device { get; set; }
    [JsonPropertyName("system_locale")] public required string SystemLocale { get; set; }

    [JsonPropertyName("browser_user_agent")]
    public required string BrowserUserAgent { get; set; }

    [JsonPropertyName("browser_version")] public required string BrowserVersion { get; set; }
    [JsonPropertyName("os_version")] public required string OperatingSystemVersion { get; set; }
    [JsonPropertyName("referrer")] public required string Referrer { get; set; }
    [JsonPropertyName("referring_domain")] public required string ReferringDomain { get; set; }

    [JsonPropertyName("referring_domain_current")]
    public required string ReferringDomainCurrent { get; set; }

    [JsonPropertyName("release_channel")] public required string ReleaseChannel { get; set; }

    [JsonPropertyName("client_build_number")]
    public required int ClientBuildNumber { get; set; }

    [JsonPropertyName("client_event_source")]
    public string? ClientEventSource { get; set; }
}

internal struct IdentifyEventPresence
{
    [JsonPropertyName("status")] public required string Status { get; set; }
    [JsonPropertyName("since")] public required int Since { get; set; }
    [JsonPropertyName("activites")] public required List<string> Activites { get; set; }
    [JsonPropertyName("afk")] public required bool AwayFromKeyboard { get; set; }
}

internal struct IdentifyEventClientState
{
    [JsonPropertyName("guild_versions")] public required Dictionary<string, string> GuildVersions { get; set; }

    [JsonPropertyName("highest_last_message_id")]
    public required string HighestLastMessageId { get; set; }

    [JsonPropertyName("read_state_version")]
    public required int ReadStateVersion { get; set; }

    [JsonPropertyName("user_guild_settings_version")]
    public required int UserGuildSettingsVersion { get; set; }

    [JsonPropertyName("user_settings_version")]
    public required int UserSettingsVersion { get; set; }

    [JsonPropertyName("private_channels_version")]
    public required string PrivateChannelsVersion { get; set; }

    [JsonPropertyName("api_client_version")]
    public required int ApiCodeVersion { get; set; }
}