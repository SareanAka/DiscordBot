using Discord;
using Discord.Interactions;
using System.Net;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Text;
using System.Text.Json;
using Sarea.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Sarea.Modules
{
    [Group("arknights", "a group of arknights commands")]
    public class ArknightsModule : InteractionModuleBase<SocketInteractionContext>
    {
        static readonly Dictionary<int, Color> _rarityColors = new()
        {
            { 0, Color.Default },
            { 1, Color.Green },
            { 2, Color.Blue },
            { 3, Color.Purple },
            { 4, Color.Gold },
            { 5, Color.Orange }
        };

        [SlashCommand("updatedata", "Checks if there is an update in the character database")]
        [RequireOwner]
        public async Task UpdateDataAsync()
        {
            // Download JSON file and save from URL
            var uri = new Uri("https://raw.githubusercontent.com/Kengxxiao/ArknightsGameData/master/en_US/gamedata/excel/character_table.json");
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                await using var fs = new FileStream("Data/Arknights/CharacterTable.json", FileMode.Create);
                {
                    await response.Content.CopyToAsync(fs);
                }
            }
            await RespondAsync("Data has been Updated.");

            await CharacterConverter.Convert();

            await ReplyAsync("Done Converting");
        }

        [SlashCommand("characterinfo", "Gives a quick overview of a character")]
        public async Task TestAsync(string name)
        {
            if (!File.Exists($"Data/Arknights/Characters/{name}.json"))
            {
                await RespondAsync($"{name} was not found");
                return;
            }
            var character = JsonSerializer.Deserialize<Character>(await File.ReadAllTextAsync($"Data/Arknights/Characters/{name}.json"));
            if (character == null)
            {
                await RespondAsync($"{name} was not found");
                return;
            }
            var characterName = character.name;
            var linkName = characterName.Replace(" ", "-").ToLower();


            var rarityColor = _rarityColors[character.rarity];

            var embed = new EmbedBuilder()
                .WithTitle($"{characterName}")
                .AddField("Rarity", $"Rarity: {character.rarity + 1}⭐")
                .AddField("Description", $"{characterName} is a {character.position.ToLower()} {character.profession.ToLower()} from the {character.subProfessionId} archetype, " +
                                         $"{character.description}\n" +
                                         $"{character.itemUsage}\n" +
                                         $"{character.itemDesc}")
                .WithColor(rarityColor)
                .WithImageUrl($"https://raw.githubusercontent.com/Aceship/Arknight-Images/main/avatars/{character.phases[2].characterPrefabKey}_2.png")
                .WithDescription($"Here is a description for {characterName}.")
                .WithUrl($"https://sareanaka.github.io/AK-Dataknights/operators/{linkName}")
                .WithCurrentTimestamp();

            await RespondAsync(embed: embed.Build());
        }
    }
}