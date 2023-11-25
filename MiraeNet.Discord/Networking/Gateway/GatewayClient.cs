using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Gateway;

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
    }

    public GatewayHandshaker Handshaker { get; }
    public GatewayHeartbeat Heartbeat { get; }
    public GatewayIdentifier Identifier { get; }
    public ILogger<GatewayClient> Logger { get; }

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
            {
                try
                {
                    var bytes = new byte[409600]; // Discord sends big messages :P
                    var message = await _socket.ReceiveAsync(bytes, default);
                    OnWebSocketMessage(message, bytes);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex,
                        "An unhandled exception occurred in the receiver thread. It has been caught and the thread will continue execution.");
                }
            }
        });
    }

    #endregion

    #region Lifecycle Methods

    public async Task StartAsync()
    {
        Logger.LogInformation("Starting Gateway client.");
        await _socket.ConnectAsync(new Uri(_gatewayUrl), default);
        OnWebSocketOpen();
    }

    public async Task StopAsync()
    {
        Logger.LogInformation("Stopping Gateway client.");
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal closure.", default);
    }

    #endregion

    #region Gateway Events Subscriber Methods

    public void SubscribeToStateChanges(Action<GatewayClientState> handler)
    {
        _stateChangeSubscriptions.Add(handler);
    }

    public void SubscribeToOpCode(int opCode, Action<IncomingPayload, WebSocketReceiveResult> handler)
    {
        _opCodeSubscriptions.Add(opCode, handler);
    }

    public void SubscribeToEvent(string eventName, Action<IncomingPayload, WebSocketReceiveResult> handler)
    {
        _eventSubscriptions.Add(eventName, handler);
    }

    #endregion

    #region WebSocket Sender Methods

    public async Task SendBytesAsync(byte[] bytes)
    {
        await _socket.SendAsync(bytes, WebSocketMessageType.Text, true, default);
    }

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
        Logger.LogInformation("Gateway connection closed: {closeDescription}", closeDescription);
        // TODO: Differentiate abnormal and graceful close
        State = GatewayClientState.Closed;
    }

    #endregion
}