using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordBot.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Check if the bot is responsive")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.RespondAsync($"?? Pong! Latency: {ctx.Client.Ping}ms");
        }

        [Command("hello")]
        [Description("Say hello to the bot")]
        public async Task Hello(CommandContext ctx)
        {
            await ctx.RespondAsync($"?? Hello {ctx.User.Mention}!");
        }

        [Command("info")]
        [Description("Get information about the bot")]
        public async Task Info(CommandContext ctx)
        {
            var embed = new DSharpPlus.Entities.DiscordEmbedBuilder
            {
                Title = "?? Bot Information",
                Description = "A Discord bot built with DSharpPlus",
                Color = DSharpPlus.Entities.DiscordColor.Blue
            };

            embed.AddField("Bot Name", ctx.Client.CurrentUser.Username, true);
            embed.AddField("Servers", ctx.Client.Guilds.Count.ToString(), true);
            embed.AddField("Latency", $"{ctx.Client.Ping}ms", true);

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
