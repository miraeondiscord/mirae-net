using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MiraeNet.Discord.Networking.Gateway;

public delegate Task SendBytesDelegate(byte[] bytes);

public class GatewayContext(ILogger logger, SendBytesDelegate sendBytesDelegate)
{
    private readonly Dictionary<string, Action<IncomingPayload, WebSocketReceiveResult>> _eventSubscriptions = new();
    private readonly Dictionary<int, Action<IncomingPayload, WebSocketReceiveResult>> _opCodeSubscriptions = new();
    private readonly List<Action<GatewayClientState>> _stateChangeSubscriptions = new();
    private GatewayClientState _state = GatewayClientState.Closed;

    public ILogger Logger { get; } = logger;

    public GatewayClientState State
    {
        get => _state;
        set
        {
            _state = value;
            foreach (var subscription in _stateChangeSubscriptions) subscription.Invoke(_state);
        }
    }

    public async Task SendPayloadAsync<TData>(OutgoingPayload<TData> payload)
    {
        var json = JsonSerializer.Serialize(payload);
        var bytes = Encoding.UTF8.GetBytes(json);
        await sendBytesDelegate(bytes);
    }

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

    public void RoutePayload(IncomingPayload payload, WebSocketReceiveResult message)
    {
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
}