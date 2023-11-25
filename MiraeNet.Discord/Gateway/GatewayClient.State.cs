namespace MiraeNet.Discord.Gateway;

public enum GatewayClientState
{
    Closed,
    Handshaking,
    Unidentified,
    Open,
    Reconnecting,
    Closing
}