using DiscordBot.Config;
using DiscordBot.Core;
using System;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var config = await BotConfig.LoadAsync();
                var bot = new Bot(config);
                await bot.StartAsync();
                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
            }
        }
    }
}