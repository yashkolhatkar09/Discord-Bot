using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

class Program
{
    static DiscordClient? client;

    static async Task Main()
    {
        // IMPORTANT: Replace this URL with your actual bot token from Discord Developer Portal
        // Go to: https://discord.com/developers/applications → Your App → Bot → Token
        var token = "MTQzNDQ1NDgzMjEzMDgyMjMyNQ.GX3SBP.99QxdSXQ7bKKGls0MiG-j8J9oTyeYkFazbuBOA"; // <-- Replace this with your real token
        
        if (string.IsNullOrWhiteSpace(token) || token == "")
        {
            Console.WriteLine("ERROR: You need to replace 'YOUR_ACTUAL_BOT_TOKEN_HERE' with your actual bot token.");
            Console.WriteLine("Get it from: https://discord.com/developers/applications");
            Console.WriteLine("Navigate to your application → Bot section → Copy Token");
            return;
        }

        client = new DiscordClient(new DiscordConfiguration
        {
            Token = token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.Guilds | 
                      DiscordIntents.GuildVoiceStates |
                      DiscordIntents.GuildMessages
        });

        client.Ready += OnClientReady;
        client.VoiceStateUpdated += OnVoiceStateUpdated;

        await client.ConnectAsync();
        await Task.Delay(-1);
    }

    private static Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
    {
        Console.WriteLine("Pilot Bot is online!");
        return Task.CompletedTask;
    }

    private static async Task OnVoiceStateUpdated(DiscordClient sender, VoiceStateUpdateEventArgs e)
    {
        // Only trigger when a user joins a voice channel
        if (e.Before?.Channel == null && e.After?.Channel != null)
        {
            var defaultTextChannel = e.Guild.Channels.Values
                .FirstOrDefault(c => c.Type == ChannelType.Text && c.PermissionsFor(e.Guild.CurrentMember).HasPermission(Permissions.SendMessages));

            if (defaultTextChannel != null)
            {
                await defaultTextChannel.SendMessageAsync($"👋 Hey {e.User.Username}, welcome to **{e.After.Channel.Name}** voice chat!");
            }
        }
    }

}
