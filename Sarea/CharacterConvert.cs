using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                foreach (var character in characters)
                {
                    var serializedCharacter = JsonConvert.SerializeObject(character.Value, Formatting.Indented);

                    await using var fs = new FileStream($"Data/Arknights/Characters/{character.Value.name}.json", FileMode.Create);
                    {
                        var info = new UTF8Encoding(true).GetBytes(serializedCharacter);
                        await fs.WriteAsync(info);
                    }
                }
        }

        private static void HandleDeserializationError(object? sender, ErrorEventArgs e)
        {
            var currentError = e.ErrorContext.Error.Message;
            e.ErrorContext.Handled = true;
        }
    }
}
