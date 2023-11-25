using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiraeNet.Discord;
using MiraeNet.Discord.Hosting;
using MiraeNet.Host;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddDiscordServices(new DiscordOptions
{
    ApiBaseUrl = builder.Configuration["Discord:ApiBaseUrl"],
    GatewayUrl = builder.Configuration["Discord:GatewayUrl"],
    Login = builder.Configuration["Discord:Login"],
    Password = builder.Configuration["Discord:Password"],
    Token = builder.Configuration["Discord:Token"]
});
builder.Services.AddHostedService<Mirae>();

var app = builder.Build();
app.Run();