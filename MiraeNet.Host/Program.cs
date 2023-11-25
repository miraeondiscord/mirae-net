using Microsoft.Extensions.Hosting;
using MiraeNet.Discord;
using MiraeNet.Discord.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddDiscordServices(new DiscordOptions());

var app = builder.Build();
app.Run();