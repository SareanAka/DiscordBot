using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Sarea.Commands.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("pong!");
        }

        [Command("echo")]
        public async Task Echo(IMessageChannel channel, [Remainder] string input)
        {
            var c = channel.Id;
            var chnl = Context.Client.GetChannel(c) as IMessageChannel;
            await chnl.SendMessageAsync(input);
            await ReplyAsync($"Message sent to <#{c}>");
        }

        [Command("info")]
        public async Task InfoAsync()
        {
            var builder = new EmbedBuilder()
                .WithColor(new Color(114, 137, 218))
                .WithTitle("Sarea")
                .WithDescription("A bot for the Sarea Discord server.")
                .AddField("Author", "Tommy")
                .AddField("Version", "0.0.1");
            await ReplyAsync("", false, builder.Build());
        }

        [Command("help")]
        public async Task HelpAsync()
        {
            var builder = new EmbedBuilder()
                .WithColor(new Color(114, 137, 218))
                .WithTitle("Help")
                .WithDescription("A list of commands for me.")
                .AddField("ping", "Pong!")
                .AddField("echo", "Echoes a message.")
                .AddField("info", "Displays information about me.")
                .AddField("help", "Displays this message.");
            await ReplyAsync("", false, builder.Build());
        }

        
        [Command("lois")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task LoisAsync(int num)
        {
            if (num >= 10)
            {
                await ReplyAsync("You can't do that!");
            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    await ReplyAsync("HOLY FUCK");
                    await ReplyAsync("I'M CUMMING");
                }
            }
        }
    }
}
