using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Gateway;

/// <summary>
///     Manages the WebSocket connection and interactions with the Discord Gateway.
/// </summary>
public class GatewayClient
{
    private readonly Dictionary<string, Action<IncomingPayload, WebSocketReceiveResult>> _eventSubscriptions = new();
    private readonly string _gatewayUrl;
    private readonly Dictionary<int, Action<IncomingPayload, WebSocketReceiveResult>> _opCodeSubscriptions = new();
    private readonly ClientWebSocket _socket;
    private readonly List<Action<GatewayClientState>> _stateChangeSubscriptions = new();
    private GatewayClientState _state = GatewayClientState.Closed;

    public GatewayClient(DiscordOptions options, ILogger<GatewayClient> logger)
    {
        Logger = logger;
        _gatewayUrl = options.GatewayUrl!;
        _socket = new ClientWebSocket();
        Heartbeat = new GatewayHeartbeat(this);
        Handshaker = new GatewayHandshaker(this);
        Identifier = new GatewayIdentifier(this);
        SubscribeToEvent("READY", (_, _) =>
        {
            Logger.LogInformation("Gateway connection is ready.");
            _state = GatewayClientState.Ready;
        });
    }

    /// <summary>
    ///     The sequence index (s field) of the last payload that had one.
    /// </summary>
    public int LastSequenceIndex { get; private set; }

    /// <summary>
    ///     Manages the handshake process with the Gateway.
    /// </summary>
    public GatewayHandshaker Handshaker { get; }

    /// <summary>
    ///     Manages the heartbeat mechanism for the Gateway connection.
    /// </summary>
    public GatewayHeartbeat Heartbeat { get; }

    /// <summary>
    ///     Responsible for identifying the client to the Gateway.
    /// </summary>
    public GatewayIdentifier Identifier { get; }

    /// <summary>
    ///     Logger for logging events and activities within the client.
    /// </summary>
    public ILogger<GatewayClient> Logger { get; }

    /// <summary>
    ///     Represents the current state of the client.
    /// </summary>
    public GatewayClientState State
    {
        get => _state;
        set
        {
            _state = value;
            foreach (var subscription in _stateChangeSubscriptions) subscription.Invoke(_state);
        }
    }

    #region WebSocket Receiver Thread

    private void Thread_WebSocketReceiver()
    {
        Task.Run(async () =>
        {
            while (_socket.State == WebSocketState.Open)
                try
                {
                    var bytes = new byte[409600]; // Discord sends big messages :P
                    var message = await _socket.ReceiveAsync(bytes, default);
                    OnWebSocketMessage(message, bytes);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex,
                        "An unhandled exception occurred in the receiver thread.\n" +
                        "It has been caught and the thread will continue execution.");
                }
        });
    }

    #endregion

    #region Lifecycle Methods

    /// <summary>
    ///     Opens the connection to the Gateway.
    /// </summary>
    public async Task StartAsync()
    {
        Logger.LogInformation("Starting Gateway client.");
        await _socket.ConnectAsync(new Uri(_gatewayUrl), default);
        OnWebSocketOpen();
    }

    /// <summary>
    ///     Closes the connection to the Gateway.
    /// </summary>
    public async Task StopAsync()
    {
        Logger.LogInformation("Stopping Gateway client.");
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal closure.", default);
    }

    #endregion

    #region Gateway Events Subscriber Methods

    /// <summary>
    ///     Subscribes to state changes of the Gateway client.
    /// </summary>
    /// <param name="handler">The handler to invoke on state changes.</param>
    public void SubscribeToStateChanges(Action<GatewayClientState> handler)
    {
        _stateChangeSubscriptions.Add(handler);
    }

    /// <summary>
    ///     Subscribes to messages with a specific op code.
    /// </summary>
    /// <param name="opCode">The op code to subscribe to.</param>
    /// <param name="handler">The handler to invoke when a message with the op code is received.</param>
    public void SubscribeToOpCode(int opCode, Action<IncomingPayload, WebSocketReceiveResult> handler)
    {
        _opCodeSubscriptions.Add(opCode, handler);
    }

    /// <summary>
    ///     Subscribes to events raised from the Gateway.
    /// </summary>
    /// <param name="eventName">The name of the event to subscribe to.</param>
    /// <param name="handler">The handler to invoke when the event is raised.</param>
    public void SubscribeToEvent(string eventName, Action<IncomingPayload, WebSocketReceiveResult> handler)
    {
        _eventSubscriptions.Add(eventName, handler);
    }

    #endregion

    #region WebSocket Sender Methods

    /// <summary>
    ///     Sends a raw byte array message through the WebSocket connection.
    /// </summary>
    /// <param name="bytes">The byte array to send.</param>
    public async Task SendBytesAsync(byte[] bytes)
    {
        await _socket.SendAsync(bytes, WebSocketMessageType.Text, true, default);
    }

    /// <summary>
    ///     Serializes and sends a payload object through the WebSocket connection.
    /// </summary>
    /// <typeparam name="TData">The type of the data in the payload.</typeparam>
    /// <param name="payload">The payload to send.</param>
    public async Task SendPayloadAsync<TData>(OutgoingPayload<TData> payload)
    {
        var json = JsonSerializer.Serialize(payload);
        var bytes = Encoding.UTF8.GetBytes(json);
        await SendBytesAsync(bytes);
    }

    #endregion

    #region WebSocket Event Handlers

    private void OnWebSocketOpen()
    {
        Logger.LogInformation("Gateway connection opened.");
        State = GatewayClientState.Open;
        Thread_WebSocketReceiver();
    }

    private void OnWebSocketMessage(WebSocketReceiveResult message, byte[] bytes)
    {
        if (message.MessageType == WebSocketMessageType.Text)
        {
            var json = Encoding.UTF8.GetString(bytes, 0, message.Count);
            var payload = JsonSerializer.Deserialize<IncomingPayload>(json)!;

            if (payload.SequenceIndex is not null)
                LastSequenceIndex = payload.SequenceIndex.Value;

            // Dispatch payloads (0) should be handled by event handlers.
            if (payload.OpCode == 0)
            {
                var validEventSubscriptions = _eventSubscriptions.Where(s => s.Key == payload.EventName!);
                foreach (var subscription in validEventSubscriptions) subscription.Value.Invoke(payload, message);
            }

            // Otherwise, other payloads should be handled by op code handlers.
            var validOpCodeSubscriptions = _opCodeSubscriptions.Where(s => s.Key == payload.OpCode);
            foreach (var subscription in validOpCodeSubscriptions) subscription.Value.Invoke(payload, message);
        }

        if (message.MessageType == WebSocketMessageType.Close)
            OnWebSocketClose(message.CloseStatus, message.CloseStatusDescription);
    }

    private void OnWebSocketClose(WebSocketCloseStatus? closeStatus, string? closeDescription)
    {
        Logger.LogInformation("Gateway connection closed: {closeStatus} {closeDescription}",
            closeStatus,
            closeDescription);
        // TODO: Differentiate abnormal and graceful close
        State = GatewayClientState.Closed;
    }

    #endregion
}