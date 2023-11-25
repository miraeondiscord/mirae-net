namespace MiraeNet.Discord.Networking.Gateway;

public enum GatewayClientState
{
    Closed,
    Open,
    Unidentified,
    Ready,
    Reconnecting,
    Closing
}