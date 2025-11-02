using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;

class Program
{
    static DiscordClient? client;

    static async Task Main()
    {
        string token = "";

        try
        {
            // Check multiple possible locations for the config file
            string configPath = "appsettings.json";

            if (!File.Exists(configPath))
            {
                // Try in the project root or current directory
                var possiblePaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"),
                    Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),
                    Path.Combine("..", "..", "..", "appsettings.json") // For Debug builds
                };

                configPath = possiblePaths.FirstOrDefault(File.Exists);

                if (configPath == null)
                {
                    Console.WriteLine("ERROR: appsettings.json not found!");
                    Console.WriteLine($"Searched in: {AppDomain.CurrentDomain.BaseDirectory}");
                    Console.WriteLine("\nCreate a file named 'appsettings.json' with this content:");
                    Console.WriteLine("{\n  \"BotToken\": \"YOUR_TOKEN_HERE\"\n}");
                    return;
                }
            }

            Console.WriteLine($"Loading config from: {Path.GetFullPath(configPath)}");

            var jsonString = await File.ReadAllTextAsync(configPath);
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

            if (config == null || !config.ContainsKey("BotToken"))
            {
                Console.WriteLine("ERROR: 'BotToken' key not found in appsettings.json");
                return;
            }

            token = config["BotToken"];
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("ERROR: appsettings.json file not found!");
            Console.WriteLine("\nCreate appsettings.json with:");
            Console.WriteLine("{\n  \"BotToken\": \"YOUR_TOKEN_HERE\"\n}");
            return;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"ERROR: Invalid JSON format in appsettings.json");
            Console.WriteLine($"Details: {ex.Message}");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR loading config: {ex.Message}");
            return;
        }

        if (string.IsNullOrWhiteSpace(token) || token == "YOUR_TOKEN_HERE")
        {
            Console.WriteLine("ERROR: Please set your bot token in appsettings.json");
            Console.WriteLine("Get it from: https://discord.com/developers/applications");
            return;
        }

        client = new DiscordClient(new DiscordConfiguration
        {
            Token = token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.Guilds |
                      DiscordIntents.GuildVoiceStates |
                      DiscordIntents.GuildMessages |
                      DiscordIntents.MessageContents  // Required to read message content
        });

        client.Ready += OnClientReady;
        client.VoiceStateUpdated += OnVoiceStateUpdated;
        client.MessageCreated += OnMessageCreated;  // Add message event handler

        await client.ConnectAsync();
        await Task.Delay(-1);
    }

    private static Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
    {
        Console.WriteLine("✅ Pilot Bot is online!");
        return Task.CompletedTask;
    }

    private static async Task OnVoiceStateUpdated(DiscordClient sender, VoiceStateUpdateEventArgs e)
    {
        // Only trigger when a user joins a voice channel
        if (e.Before?.Channel == null && e.After?.Channel != null)
        {
            var defaultTextChannel = e.Guild.Channels.Values
                .FirstOrDefault(c => c.Type == ChannelType.Text &&
                                   c.PermissionsFor(e.Guild.CurrentMember).HasPermission(Permissions.SendMessages));


            if (defaultTextChannel != null)
            {
                await defaultTextChannel.SendMessageAsync($"👋 Hey {e.User.Mention}, welcome to **{e.After.Channel.Name}** voice chat!");
            }
        }
    }

    private static async Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
    {
        // Ignore messages from bots (including itself)
        if (e.Author.IsBot) return;

        // Check if the bot is mentioned
        if (e.MentionedUsers.Any(u => u.Id == sender.CurrentUser.Id))
        {
            Console.WriteLine($"Bot was mentioned by {e.Author.Username} in #{e.Channel.Name}");

            // Respond to the mention
            await e.Message.RespondAsync($"Hey {e.Author.Mention}! You mentioned me. How can I help?");
        }

        // Alternative: Check if message starts with a mention of the bot
        if (e.Message.Content.StartsWith($"<@{sender.CurrentUser.Id}>") ||
            e.Message.Content.StartsWith($"<@!{sender.CurrentUser.Id}>"))
        {
            Console.WriteLine($"Bot was directly mentioned at start of message");
        }
    }
}