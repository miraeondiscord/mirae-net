namespace MiraeNet.Discord.Networking.Gateway;

/// <summary>
///     Represents the different states of a Gateway client's connection to the Discord Gateway.
/// </summary>
public enum GatewayClientState
{
    /// <summary>
    ///     The connection is closed.
    /// </summary>
    Closed,

    /// <summary>
    ///     The connection is open but not ready.
    /// </summary>
    Open,

    /// <summary>
    ///     The connection is open but not the client has not yet identified with the Gateway.
    /// </summary>
    Unidentified,

    /// <summary>
    ///     Indicates that the connection is open and fully ready.
    /// </summary>
    Ready,

    /// <summary>
    ///     Indicates that the client is attempting to reconnect.
    /// </summary>
    Reconnecting,

    /// <summary>
    ///     Indicates that the connection is gracefully closing.
    /// </summary>
    Closing
}