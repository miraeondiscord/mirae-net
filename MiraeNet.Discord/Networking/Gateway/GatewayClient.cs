using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Gateway;

public class GatewayClient
{
    private readonly GatewayContext _context;
    private readonly ClientWebSocket _socket;
    private readonly GatewayHeartbeat _heartbeat;
    private readonly GatewayHandshaker _handshaker;
    private readonly string _gatewayUrl;

    public GatewayClient(string gatewayUrl, ILogger<GatewayClient> logger)
    {
        _gatewayUrl = gatewayUrl;
        _context = new GatewayContext(logger, SendBytesAsync);
        _socket = new ClientWebSocket();
        _heartbeat = new GatewayHeartbeat(_context);
        _handshaker = new GatewayHandshaker(_context);
    }

    #region WebSocket Receiver Thread

    private void Thread_WebSocketReceiver()
    {
        // TODO: Handle thread crashes
        Task.Run(async () =>
        {
            while (_socket.State == WebSocketState.Open)
            {
                var bytes = new byte[409600]; // Discord sends big messages :P
                var message = await _socket.ReceiveAsync(bytes, default);
                OnWebSocketMessage(message, bytes);
            }
        });
    }

    #endregion

    #region Public Methods

    public async Task StartAsync()
    {
        _context.Logger.LogInformation("Starting Gateway client.");
        await _socket.ConnectAsync(new Uri(_gatewayUrl), default);
        OnWebSocketOpen();
    }

    public async Task StopAsync()
    {
        _context.Logger.LogInformation("Stopping Gateway client.");
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal closure.", default);
    }

    #endregion

    #region WebSocket Sender Methods

    private async Task SendBytesAsync(byte[] bytes)
    {
        await _socket.SendAsync(bytes, WebSocketMessageType.Text, true, default);
    }

    #endregion

    #region WebSocket Event Handlers

    private void OnWebSocketOpen()
    {
        _context.Logger.LogInformation("Gateway connection opened.");
        _context.State = GatewayClientState.Open;
        Thread_WebSocketReceiver();
    }

    private void OnWebSocketMessage(WebSocketReceiveResult message, byte[] bytes)
    {
        if (message.MessageType == WebSocketMessageType.Text)
        {
            var json = Encoding.UTF8.GetString(bytes, 0, message.Count);
            var payload = JsonSerializer.Deserialize<IncomingPayload>(json);
            _context.RoutePayload(payload!, message);
        }

        if (message.MessageType == WebSocketMessageType.Close)
            OnWebSocketClose(message.CloseStatus, message.CloseStatusDescription);
    }

    private void OnWebSocketClose(WebSocketCloseStatus? closeStatus, string? closeDescription)
    {
        _context.Logger.LogInformation("Gateway connection closed: {closeDescription}", closeDescription);
        // TODO: Differentiate abnormal and graceful close
        _context.State = GatewayClientState.Closed;
    }

    #endregion
}