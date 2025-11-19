using System.Text.Json;

namespace DiscordBot.Config
{
    public class BotConfig
    {
        public string Token { get; set; } = string.Empty;
        public string Prefix { get; set; } = "!";

        public static async Task<BotConfig> LoadAsync()
        {
            //
            // 1️⃣ FIRST: Try to load token from Environment Variables (Railway / Docker)
            //
            string? envToken = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
            string? envPrefix = Environment.GetEnvironmentVariable("PREFIX");

            if (!string.IsNullOrEmpty(envToken))
            {
                Console.WriteLine("🔐 Loaded bot token from environment variable.");

                return new BotConfig
                {
                    Token = envToken,
                    Prefix = !string.IsNullOrEmpty(envPrefix) ? envPrefix : "!"
                };
            }

            //
            // 2️⃣ SECOND: Fallback to appsettings.json (Local development)
            //
            string? configPath = FindConfigFile();

            if (configPath == null)
            {
                throw new FileNotFoundException(
                    "❌ appsettings.json not found!\n\n" +
                    "Create this file in your project root:\n\n" +
                    "{\n" +
                    "  \"BotToken\": \"YOUR_TOKEN_HERE\",\n" +
                    "  \"Prefix\": \"!\"\n" +
                    "}"
                );
            }

            Console.WriteLine($"📁 Loading config from: {Path.GetFullPath(configPath)}");

            var jsonString = await File.ReadAllTextAsync(configPath);
            var configDict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

            if (configDict == null || !configDict.ContainsKey("BotToken"))
            {
                throw new Exception("❌ 'BotToken' key not found in appsettings.json");
            }

            return new BotConfig
            {
                Token = configDict["BotToken"],
                Prefix = configDict.ContainsKey("Prefix") ? configDict["Prefix"] : "!"
            };
        }

        //
        // 🔍 Helper to find appsettings.json during local runs
        //
        private static string? FindConfigFile()
        {
            var possiblePaths = new[]
            {
                "appsettings.json",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),
                Path.Combine("..", "..", "..", "appsettings.json") // Debug mode
            };

            return possiblePaths.FirstOrDefault(File.Exists);
        }
    }
}
