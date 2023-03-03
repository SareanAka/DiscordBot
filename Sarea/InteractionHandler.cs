using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

namespace Sarea
{
    public class InteractionHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly IServiceProvider _service;

        public InteractionHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider service)
        {
            _client = client;
            _commands = commands;
            _service = service;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _service);

            _client.InteractionCreated += HandleInteractionAsync;
        }

        private async Task HandleInteractionAsync(SocketInteraction interaction)
        {
            try
            {
                _client.AutocompleteExecuted += async arg => {
                    var context = new InteractionContext(_client, arg, arg.Channel);
                    await _commands.ExecuteCommandAsync(context, services: _service);
                };
                var ctx = new SocketInteractionContext(_client, interaction);
                await _commands.ExecuteCommandAsync(ctx, _service);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }
}
