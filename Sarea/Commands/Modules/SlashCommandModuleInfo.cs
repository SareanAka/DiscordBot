using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Interactions;

namespace Sarea.Commands.Modules
{
    internal class SlashCommandModuleInfo : ModuleBase<SocketCommandContext>
    {
        [SlashCommand("first-command", "This is my first guild slash command!")]
        public async Task FirstCommandAsync()
        {
            await ReplyAsync("This is my first guild slash command!");
        }

        [SlashCommand("lois", "Holy Fuck!")]
        [Discord.Interactions.RequireUserPermission(GuildPermission.Administrator)]
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
