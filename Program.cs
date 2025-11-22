using DiscordBot.Config;
using DiscordBot.Core;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace DiscordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Start health check server in background
                var healthCheckTask = Task.Run(() => StartHealthCheckServer(args));

                // Start Discord bot
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

        static void StartHealthCheckServer(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Get port from environment variable or use default
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(int.Parse(port));
            });

            var app = builder.Build();

            app.MapGet("/", () => "Bot is alive!");

            Console.WriteLine($"Health check server listening on port {port}");
            app.Run();
        }
    }
}