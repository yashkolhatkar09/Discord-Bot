using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordBot.Config
{
  public class BotConfig
    {
        public string Token { get; set; } = string.Empty;
   public string Prefix { get; set; } = "!";

        public static async Task<BotConfig> LoadAsync()
    {
            string? configPath = FindConfigFile();

       if (configPath == null)
        {
      throw new FileNotFoundException(
    "appsettings.json not found!\n" +
  "Create it with:\n" +
          "{\n  \"BotToken\": \"YOUR_TOKEN_HERE\",\n  \"Prefix\": \"!\"\n}"
        );
       }

            Console.WriteLine($"📁 Loading config from: {Path.GetFullPath(configPath)}");

       var jsonString = await File.ReadAllTextAsync(configPath);
            var configDict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

 if (configDict == null || !configDict.ContainsKey("BotToken"))
  {
    throw new Exception("'BotToken' key not found in appsettings.json");
      }

            return new BotConfig
            {
      Token = configDict["BotToken"],
  Prefix = configDict.ContainsKey("Prefix") ? configDict["Prefix"] : "!"
   };
        }

        private static string? FindConfigFile()
        {
     var possiblePaths = new[]
          {
"appsettings.json",
       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"),
            Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),
                Path.Combine("..", "..", "..", "appsettings.json")
      };

            return possiblePaths.FirstOrDefault(File.Exists);
        }
    }
}