using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace DiscordBot.Events
{
    public static class MessageEvent
    {
        public static async Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
        {
            if (e.Author.IsBot) return;

            // Log mentions
            if (e.MentionedUsers.Any(u => u.Id == sender.CurrentUser.Id))
            {
                Console.WriteLine($"📢 Mentioned by {e.Author.Username} in #{e.Channel.Name}");
            }

            // You can add custom message handling here
            // For commands, use the Commands system instead
        }
    }
}