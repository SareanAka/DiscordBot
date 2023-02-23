using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.Yaml;
using Newtonsoft.Json;
using Sarea;

public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddYamlFile("config.yml")
            .Build();

        using IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
                services
                    .AddSingleton(config)
                    .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
                    {
                        GatewayIntents = GatewayIntents.AllUnprivileged,
                        LogGatewayIntentWarnings = false,
                        LogLevel = LogSeverity.Info,
                        AlwaysDownloadUsers = true
                    }))
                    .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                    .AddSingleton<InteractionHandler>()
            )
            .Build();

        await RunAsync(host);
    }

    public async Task RunAsync(IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        var _client = services.GetRequiredService<DiscordSocketClient>();
        var _commands = services.GetRequiredService<InteractionService>();
        await services.GetRequiredService<InteractionHandler>().InitializeAsync();
        var config = services.GetRequiredService<IConfigurationRoot>();

        _client.Log += async (LogMessage msg) => { Console.WriteLine(msg.Message); };
        _commands.Log += async (LogMessage msg) => { Console.WriteLine(msg.Message); };

        _client.Ready += async () =>
        {
            Console.WriteLine("Bot is connected!");
            await _commands.RegisterCommandsToGuildAsync(UInt64.Parse(config["guild_id"]), true);
        };

        await _client.LoginAsync(TokenType.Bot, config["tokens:discord"]);
        await _client.StartAsync();

        await Task.Delay(-1);
    }
}