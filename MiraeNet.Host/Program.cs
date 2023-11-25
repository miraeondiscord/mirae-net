using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiraeNet.Core;
using MiraeNet.Discord;
using MiraeNet.Host;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddCore();
builder.Services.AddDiscord(new DiscordOptions
{
    ApiBaseUrl = builder.Configuration["Discord:ApiBaseUrl"],
    GatewayUrl = builder.Configuration["Discord:GatewayUrl"],
    Login = builder.Configuration["Discord:Login"],
    Password = builder.Configuration["Discord:Password"],
    Token = builder.Configuration["Discord:Token"]
});
builder.Services.AddHostedService<Service>();

var app = builder.Build();
app.Run();