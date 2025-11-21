using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Linq; // For .Where()
using System.Net.Http;

namespace DiscordBot.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        private static readonly HttpClient httpClient = new HttpClient();

        [Command("ping")]
        [Description("Check if the bot is responsive")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.RespondAsync($"Pong! Latency: {ctx.Client.Ping}msH");
        }

        [Command("hello")]
        [Description("Say hello to the bot")]
        public async Task Hello(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Hello {ctx.User.Mention}!");
        }

        [Command("info")]
        [Description("Get information about the bot")]
        public async Task Info(CommandContext ctx)
        {
            var embed = new DSharpPlus.Entities.DiscordEmbedBuilder
            {
                Title = "Bot Information",
                Description = "A Discord bot built with DSharpPlus",
                Color = DSharpPlus.Entities.DiscordColor.Red
            };
            embed.AddField("Bot Name", ctx.Client.CurrentUser.Username, true);
            embed.AddField("Servers", ctx.Client.Guilds.Count.ToString(), true);
            embed.AddField("Latency", $"{ctx.Client.Ping}ms", true);
            embed.AddField("User", $"{ctx.User.Mention}", true);
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("ascii")]
        [Description("Convert text to ASCII art")]
        public async Task Ascii(CommandContext ctx, [RemainingText] string text)
        {
            // Check if text was provided
            if (string.IsNullOrWhiteSpace(text))
            {
                await ctx.RespondAsync("❌ Please provide text to convert! Usage: `!ascii your text here`");
                return;
            }

            try
            {
                // Delete the user's message
                await ctx.Message.DeleteAsync();


                // URL encode the text and make the API request
                string encodedText = Uri.EscapeDataString(text);
                string apiUrl = $"https://asciified.thelicato.io/api/v2/ascii?text={encodedText}&font=Pagga";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string asciiArt = await response.Content.ReadAsStringAsync();

                // Delete the thinking message
                //await thinkingMsg.DeleteAsync();

                // Send the ASCII art in a code block for proper formatting
                await ctx.RespondAsync($"```\n{asciiArt}\n```");
            }
            catch (HttpRequestException ex)
            {
                await ctx.RespondAsync($"❌ Failed to fetch ASCII art: {ex.Message}");
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync($"❌ An error occurred: {ex.Message}");
            }
        }

        [Command("kickall")]
        [Description("Kick everyone from your current voice channel")]
        [RequirePermissions(Permissions.MoveMembers)]
        public async Task KickAll(CommandContext ctx)
        {
            // Check if the command user is in a voice channel
            if (ctx.Member?.VoiceState?.Channel == null)
            {
                await ctx.RespondAsync("❌ You must be in a voice channel to use this command!");
                return;
            }
            
            var voiceChannel = ctx.Member.VoiceState.Channel;
            
            // Check if bot has permission
            var botPermissions = voiceChannel.PermissionsFor(ctx.Guild.CurrentMember);
            if (!botPermissions.HasPermission(Permissions.MoveMembers))
            {
                //await ctx.RespondAsync("❌ I don't have permission to move members!");
                return;
            }
            
            // Get all members except the bot
            var members = voiceChannel.Users.Where(u => !u.IsBot).ToList();
            
            if (members.Count == 0)
            {
                //await ctx.RespondAsync("❌ No one to kick from the voice channel!");
                return;
            }
            
            // Send confirmation
            //await ctx.RespondAsync($"⏳ Kicking {members.Count} members from **{voiceChannel.Name}**...");
            
            // Kick everyone
            int kicked = 0;
            foreach (var user in members)
            {
                try
                {
                    await user.ModifyAsync(x => x.VoiceChannel = null);
                    kicked++;
                }
                catch (Exception ex)
                {
                    // Log error but continue
                    Console.WriteLine($"Failed to kick {user.Username}: {ex.Message}");
                }
            }
        }

    }
}