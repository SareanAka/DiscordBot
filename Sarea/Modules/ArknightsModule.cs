using Discord;
using Discord.Interactions;
using System.Net;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Text;
using System.Text.Json;

namespace Sarea.Modules
{
    [Group("arknights", "a group of arknights commands")]
    public class ArknightsModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("updatedata", "Checks if there is an update in the character database")]
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
        }

        [SlashCommand("test", "test")]
        public async Task TestAsync(String arg)
        {
            Character? character = new Character();
            string fileName = $"Data/Arknights/{arg}.json";
            using FileStream openStream = File.OpenRead(fileName);
            character = await JsonSerializer.DeserializeAsync<Character>(openStream)!;

            Color color = Color.Teal;
            switch (character?.rarity)
            {
                case 3:
                {
                    color = Color.Blue;
                    break;
                }
                case 4:
                {
                    color = Color.Purple;
                    break;
                }
                case 5:
                {
                    color = Color.Gold;
                    break;
                }
                case 6:
                {
                    color = Color.LightOrange;
                    break;
                }
            }

            var characterName = character.key;
            if (characterName.Contains("-"))
            {
                characterName = characterName.Replace("-", " ");
            }

            characterName = char.ToUpper(characterName[0]) + characterName.Substring(1);

            var embed = new EmbedBuilder()
                .WithTitle($"{characterName}")
                .WithFooter(footer => footer.Text = $"{character?.id}")
                .AddField("Rarity", $"Rarity: {character?.rarity}⭐")
                .AddField("Description", $"{characterName} is a {character?.position.ToLower()} {character?._class.ToLowerInvariant()} from the {character?.classBranch} archetype")
                .WithColor(color)
                .WithImageUrl($"https://raw.githubusercontent.com/Aceship/Arknight-Images/main/avatars/{character?.phases[2].outfit.portraitId}.png")
                .WithDescription($"Here is a description for {characterName}.")
                .WithUrl($"https://sareanaka.github.io/AK-Dataknights/operators/{character?.key}")
                .WithCurrentTimestamp();

            await RespondAsync(embed: embed.Build());
        }
    }
}