namespace MiraeNet.Discord.Gateway;

public partial class GatewayClient
{
    public async Task SetStatusAsync(string status, string customStatus)
    {
        var data = new Dictionary<string, dynamic>
        {
            { "status", status },
            { "since", 0 },
            {
                "activities", new List<Dictionary<string, dynamic?>>
                {
                    new()
                    {
                        { "name", "Custom Status" },
                        { "type", 4 },
                        { "state", customStatus },
                        { "emoji", null }
                    }
                }
            },
            { "afk", false }
        };
        var payload = new OutgoingPayload<Dictionary<string, dynamic>>
        {
            OpCode = 3,
            Data = data
        };
        await SendPayloadAsync(payload);
    }
}