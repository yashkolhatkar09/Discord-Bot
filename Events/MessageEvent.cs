using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace DiscordBot.Events
{
    public static class MessageEvent
    {
        private static readonly HashSet<string> BadWords = new()
        {
            "job",
            "internship"
        };

        public static async Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
        {
            if (e.Author.IsBot) return;

            if (ContainsBadWord(e.Message.Content))
            {
                await TryDelete(e.Message);
            }
        }

        public static async Task OnMessageUpdated(DiscordClient sender, MessageUpdateEventArgs e)
        {
            // Ignore bot edits
            if (e.Author.IsBot) return;

            // e.Message.Content may be null on edit events ? use e.MessageBefore if needed
            string? afterContent = e.Message?.Content;
            if (afterContent == null) return;

            if (ContainsBadWord(afterContent))
            {
                await TryDelete(e.Message);
            }
        }

        private static async Task TryDelete(DiscordMessage msg)
        {
            try
            {
                await msg.DeleteAsync("Updated/Edited message contains banned words.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete failed: " + ex.Message);
            }
        }

        private static bool ContainsBadWord(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return false;

            var cleanedContent = content.ToLower()
                .Replace("0", "o")
                .Replace("1", "i")
                .Replace("3", "e")
                .Replace("4", "a")
                .Replace("5", "s")
                .Replace("7", "t")
                .Replace("8", "b")
                .Replace("@", "a")
                .Replace("$", "s")
                .Replace("!", "i")
                .Replace("|", "i")
                .Replace("9", "g");

            cleanedContent = new string(cleanedContent.Where(c => char.IsLetterOrDigit(c)).ToArray());

            return BadWords.Any(bad => cleanedContent.Contains(bad));
        }
    }
}
