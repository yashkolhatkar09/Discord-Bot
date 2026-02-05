using DiscordBot.Config;
using DiscordBot.Core;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Start health check server in background (non-blocking)
            var healthCheckTask = Task.Run(() => StartHealthCheckServer(args));

            // Give health check server time to start
            await Task.Delay(2000);

            // Start Discord bot with retry logic (won't crash the app)
            await StartDiscordBotWithRetry();
        }

        static async Task StartDiscordBotWithRetry()
        {
            int retryCount = 0;
            int maxRetries = 10;

            while (true)
            {
                try
                {
                    retryCount++;
                    Console.WriteLine($"🔄 Starting Discord bot (Attempt {retryCount})");

                    var config = await BotConfig.LoadAsync();
                    var bot = new Bot(config);
                    await bot.StartAsync();

                    Console.WriteLine("✅ Discord bot is running!");

                    // Keep the bot running indefinitely
                    await Task.Delay(-1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Discord bot failed: {ex.GetType().Name}");
                    Console.WriteLine($"   Message: {ex.Message}");

                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"   Inner: {ex.InnerException.Message}");
                    }

                    // Calculate exponential backoff: 60s, 120s, 240s, 480s (max 8min)
                    int waitSeconds = Math.Min(60 * (int)Math.Pow(2, Math.Min(retryCount - 1, 3)), 480);
                    
                    Console.WriteLine($"⏳ Health check server still running. Retrying Discord connection in {waitSeconds}s...");
                    Console.WriteLine($"💡 Tip: Rate limits usually clear in 10-15 minutes");
                    
                    await Task.Delay(waitSeconds * 1000);
                }
            }
        }

        static void StartHealthCheckServer(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Get port from environment variable or fallback to 8080
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

                // Suppress noisy logs
                builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);

                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.ListenAnyIP(int.Parse(port));
                });

                var app = builder.Build();

                app.MapMethods("/", new[] { "GET", "HEAD" }, () => "Bot is alive!");

                Console.WriteLine($"🌐 Health check server listening on port {port}");
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Health check server failed: {ex.Message}");
                
                // Keep process alive even if health check fails
                Task.Delay(-1).GetAwaiter().GetResult();
            }
        }
    }
}