using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using MiraeNet.Core.Discord;

namespace MiraeNet.Discord.Gateway;

public partial class GatewayClient
{
    private readonly Dictionary<string, Action<IncomingPayload>> _eventHandlers;
    private readonly string _gatewayUrl;
    private readonly ILogger<GatewayClient> _logger;
    private readonly Dictionary<int, Action<IncomingPayload>> _opCodeHandlers;

    private int _lastSequenceIndex;
    private ClientWebSocket _socket;
    private GatewayClientState _state;

    public GatewayClient(string gatewayUrl, ILogger<GatewayClient> logger)
    {
        _logger = logger;
        _socket = new ClientWebSocket();
        _gatewayUrl = gatewayUrl;
        _opCodeHandlers = GetOpCodeHandlers(this); // Defined in GatewayClient.Handlers.cs.
        _eventHandlers = GetEventHandlers(this); // Defined in GatewayClient.Events.cs.
        _state = GatewayClientState.Closed;
        _lastSequenceIndex = 0;
    }

    public event Action? Opened;
    public event Action? Readied;
    public event Action? Reconnecting;
    public event Action? Closed;
    public event Action<Message>? MessageCreated;

    public async Task StartAsync()
    {
        _logger.LogInformation("Starting Gateway connection.");
        await _socket.ConnectAsync(new Uri(_gatewayUrl), default);
        HandleWebSocketOpen();
    }

    public async Task StopAsync()
    {
        _logger.LogInformation("Stopping Gateway connection.");
        _state = GatewayClientState.Closing;
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal closure.", default);
    }

    private async Task SendBytesAsync(byte[] bytes)
    {
        await _socket.SendAsync(bytes, WebSocketMessageType.Text, true, default);
    }

    private async Task SendPayloadAsync<TData>(OutgoingPayload<TData> payload)
    {
        var json = JsonSerializer.Serialize(payload);
        var bytes = Encoding.UTF8.GetBytes(json);
        await SendBytesAsync(bytes);
    }

    private void HandleWebSocketOpen()
    {
        _state = GatewayClientState.Handshaking;
        Thread_WebSocketReceive();
        Opened?.Invoke();
    }

    private void HandleWebSocketMessage(WebSocketReceiveResult message, byte[] bytes)
    {
        if (message.MessageType == WebSocketMessageType.Text)
        {
            var json = Encoding.UTF8.GetString(bytes, 0, message.Count);
            var payload = JsonSerializer.Deserialize<IncomingPayload>(json);
            HandleIncomingPayload(payload!); // Defined in GatewayClient.Handlers.cs.
        }

        if (message.MessageType == WebSocketMessageType.Close)
            HandleWebSocketClose(message.CloseStatus, message.CloseStatusDescription);
    }

    private void HandleWebSocketClose(WebSocketCloseStatus? closeStatus, string? closeDescription)
    {
        // Defined in GatewayClient.Heartbeat.cs.
        if (_heartbeatTimer is not null)
        {
            _heartbeatTimer.Dispose();
            _heartbeatTimer = null;
        }

        if (_state == GatewayClientState.Closing)
        {
            Closed?.Invoke();
            return;
        }

        _logger.LogWarning(
            "Gateway connection closed abnormally. Will attempt to reconnect.\n{closeStatus} {closeDescription}",
            closeStatus,
            closeDescription);

        Reconnecting?.Invoke();
        _socket = new ClientWebSocket();
        _ = StartAsync();
    }

    private void Thread_WebSocketReceive()
    {
        Task.Run(async () =>
        {
            while (_socket.State == WebSocketState.Open)
            {
                var bytes = new byte[409600]; // Discord sends big messages :P
                var message = await _socket.ReceiveAsync(bytes, default);
                HandleWebSocketMessage(message, bytes);
            }
        });
    }
}