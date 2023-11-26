using Microsoft.Extensions.Logging;
using MiraeNet.Core.Completion;
using MiraeNet.Core.Discord;

namespace MiraeNet.Core;

/// <summary>
///     Encapsulates the core business logic that drives the Mirae
///     agent's behavior.
/// </summary>
public class Agent
{
    private string _currentChannelId = string.Empty;
    private readonly List<Message> _conversation = new();
    private readonly SemaphoreSlim _semaphore = new(0, 1);
    private readonly AgentOptions _options;
    private readonly ILifecycleManager _discord;
    private readonly IEventService _eventService;
    private readonly ILogger<Agent> _logger;

    /// <summary>
    ///     Instantiates a new instance of the <see cref="Agent" /> class.
    /// </summary>
    /// <param name="options">TODO: Write docs</param>
    /// <param name="discord"></param>
    /// <param name="channelService"></param>
    /// <param name="eventService"></param>
    /// <param name="completionService"></param>
    /// <param name="logger">The logger to use for logging agent information.</param>
    public Agent(
        AgentOptions options,
        ILifecycleManager discord,
        IChannelService channelService,
        IEventService eventService,
        ICompletionService completionService,
        ILogger<Agent> logger)
    {
        _options = options;
        _discord = discord;
        _eventService = eventService;
        _logger = logger;
    }

    public async Task StartAsync()
    {
        _logger.LogInformation("💖 Mirae 💖");
        _logger.LogInformation("Starting Agent.\n");
        _eventService.MessageCreated += OnMessageCreated;
        await _discord.StartAsync();
    }

    public Task StopAsync()
    {
        _logger.LogInformation("Stopping Agent.\n");
        return _discord.StopAsync();
    }

    private void OnMessageCreated(Message message)
    {
        _semaphore.Wait();

        // Determine if
    }
}