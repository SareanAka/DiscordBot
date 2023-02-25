using Discord;
using Discord.Interactions;

namespace Sarea.Modules
{
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        

        private Dictionary<string, List<IEmote>> _seggs = new()
        {
            {"steamy!", new List<IEmote> { new Emoji("😩"), new Emoji("🥵"), new Emoji("🍆"), new Emoji("💦"), new Emoji("🍑")}},
            {":b:ased!", new List<IEmote> { new Emoji("💯"), new Emoji("🔞"), new Emoji("👌"), new Emoji("💅"), new Emoji("😼") }},
            {"cringe...", new List<IEmote> { new Emoji("😔"), new Emoji("💢"), new Emoji("😞"), new Emoji("🫥"), new Emoji("🤨") }},
            {"UUOOOOHHH!", new List<IEmote> { new Emoji("😭"), new Emoji("🥵"), new Emoji("🤓"), new Emoji("🤤"), new Emoji("😤") }},
        };
        
        private Random _rand = new();

        [SlashCommand("ping", "pong!")]
        public async Task PingAsync()
        {
            await RespondAsync("Pong!");
        }
        
        [SlashCommand("sex", "HE'S PULLING HIS COCK OUT!!! ( ͡°( ͡° ͜ʖ( ͡° ͜ʖ ͡°)ʖ ͡°) ͡°)")]
        [RequireOwner]
        public async Task SexAsync(IGuildUser user, IGuildUser user2, IGuildUser user3, IGuildUser? user4 = null)
        {
            var seggs = _seggs.ElementAt(_rand.Next(_seggs.Count));

            var reply = await ReplyAsync($"{user.Mention} just had sex with {user2.Mention}, it was very {seggs.Key}\n" +
                                         $"{user3.Mention} {(user4 != null ? $"and {user4.Mention} were" : "was")} watching.");

            await reply.AddReactionsAsync(seggs.Value);
        }
    }
}
