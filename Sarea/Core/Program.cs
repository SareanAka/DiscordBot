using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Sarea.Commands;
using Sarea.Core;

public class Program
{
    private DiscordSocketClient _client;
    private readonly IServiceProvider _serviceProvider;

    public Program()
    {
        _client = new DiscordSocketClient();
        _serviceProvider = CreateProvider();
    }
    static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

    static IServiceProvider CreateProvider()
    {
        var config = new DiscordSocketConfig()
        {

        };
        var collection = new ServiceCollection()
            .AddSingleton(config)
            .AddSingleton<DiscordSocketClient>();
        
        return collection.BuildServiceProvider();
    }

    public async Task MainAsync()
    {
        var config = new DiscordSocketConfig { MessageCacheSize = 100 };
        _client = new DiscordSocketClient(config);

        if (Environment.GetEnvironmentVariable("TOKEN") == null)
        {
            string _token;
            // get token from txt file
            using (StreamReader sr = new StreamReader(@"C:/Users/tommy/source/repos/Sarea/Sarea/token.txt"))
            {
                _token = sr.ReadToEnd();
            }

            Environment.SetEnvironmentVariable("TOKEN", _token);

        }
        
        
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN"));
        await _client.StartAsync();

        _client.MessageUpdated += MessageUpdated;
        _client.Ready += () =>
        {
            Console.WriteLine("Bot is connected!");
            return Task.CompletedTask;
        };

        // Block this task until the program is closed.
        await Task.Delay(Timeout.Infinite);
    }

    private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
    {
        var message = await before.GetOrDownloadAsync();
        Console.WriteLine($"{message} -> {after}");
    }
}