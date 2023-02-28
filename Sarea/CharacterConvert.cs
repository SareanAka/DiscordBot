using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sarea.Models;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace Sarea
{
    public class CharacterConverter
    {
        public static async Task Convert()
        {
            var charactersJson = await File.ReadAllTextAsync("Data/Arknights/CharacterTable.json");
            var characters = JsonConvert.DeserializeObject<Dictionary<string, Character>>(charactersJson, new JsonSerializerSettings
            {
                Error = HandleDeserializationError
            });
            if (characters != null)
            {
                foreach (var character in characters)
                {
                    var serializedCharacter = JsonConvert.SerializeObject(character.Value, Formatting.Indented);

                    await using var fs = new FileStream($"Data/Arknights/Characters/{character.Value.name}.json",
                        FileMode.Create);
                    {
                        var info = new UTF8Encoding(true).GetBytes(serializedCharacter);
                        await fs.WriteAsync(info);
                    }
                }

                var sb = new StringBuilder();
                foreach (var character in characters)
                {
                    var isValid = await IsCharacterValid($"https://raw.githubusercontent.com/Aceship/Arknight-Images/main/avatars/{character.Value.phases[0].characterPrefabKey}_2.png");
                    if (!isValid)
                    {
                        continue;
                    }
                    var name = JsonConvert.SerializeObject(character.Value.name);
                    sb.Insert(0, name + ": " + $"\"https://raw.githubusercontent.com/Aceship/Arknight-Images/main/avatars/{character.Value.phases[0].characterPrefabKey}_2.png\", \n");
                }
                await using var fileStream = new FileStream("Data/Arknights/CharacterList.json", FileMode.Create);
                {
                    var info = new UTF8Encoding(true).GetBytes("{\n" + sb + '}');
                    await fileStream.WriteAsync(info);
                }
            }
        }

        private static void HandleDeserializationError(object? sender, ErrorEventArgs e)
        {
            e.ErrorContext.Handled = true;
        }

        private static async Task<bool> IsCharacterValid(string uri)
        {
            try
            {
                Console.WriteLine($"Checking {uri}");
                var request = new HttpClient();

                using var responseStream = await request.GetAsync(uri);
                return responseStream.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
