using System.Text;
using Newtonsoft.Json;
using Sarea.Models;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace Sarea
{
    public class CharacterConverter
    {
        private static int _validCharCount;
        private static int _progress;
        
        public static async Task Convert()
        {
            _validCharCount = 0;
            _progress = 0;
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

                Console.WriteLine($"Checking validity of {characters.Count} characters");
                var sb = new StringBuilder();
                foreach (var character in characters)
                {
                    _progress++;
                    Console.Write($"\r{0}% Checking: {_progress}/{characters.Count}...");
                    var isValid = await IsCharacterValid($"https://raw.githubusercontent.com/Aceship/Arknight-Images/main/avatars/{character.Value.phases[0].characterPrefabKey}_2.png");
                    if (!isValid)
                    {
                        continue;
                    }
                    var name = JsonConvert.SerializeObject(character.Value.name);
                    sb.Insert(0, name + ",\n");
                    _validCharCount++;
                }
                Console.WriteLine($"Found {_validCharCount} valid characters.");
                await using var fileStream = new FileStream("Data/Arknights/CharacterList.json", FileMode.Create);
                {
                    var info = new UTF8Encoding(true).GetBytes("[\n" + sb + ']');
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
