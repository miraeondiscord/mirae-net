using Microsoft.Extensions.Logging;
using MiraeNet.Core.Completion;
using MiraeNet.Core.Discord;

namespace MiraeNet.Core;

public class Agent
{
    private readonly ILifecycleManager _discord;
    private readonly ILogger<Agent> _logger;

    public Agent(ILifecycleManager discord, ILogger<Agent> logger, IChannelService channelService,
        IEventService eventService, ICompletionService completionService)
    {
        _discord = discord;
        _logger = logger;
        eventService.MessageCreated += async message =>
        {
            // if (message.Author.Id == _discord.CurrentUser.Id)
            //     return;
            // var channelId = message.ChannelId;
            // var content = message.Content ?? "null";
            // var author = message.Author.Username;
            // _logger.LogInformation("{author}: {content}", author, content);
            // var completionMessage = MessageConverter.Convert(message, _discord.CurrentUser.Id);
            // var systemMessage = new CompletionMessage
            // {
            //     Role = CompletionMessageRole.System,
            //     Content = 
            // };
            // var convo = new List<CompletionMessage>() { systemMessage, completionMessage };
            // var generation = await completionService.CreateCompletionAsync(convo);
            // await channelService.CreateMessageAsync(channelId, generation.Content);
        };
    }

    public Task StartAsync()
    {
        _logger.LogInformation("💖 Mirae 💖");
        _logger.LogInformation("Starting Agent.\n");
        return _discord.StartAsync();
    }

    public Task StopAsync()
    {
        _logger.LogInformation("Stopping Agent.");
        return _discord.StopAsync();
    }
}