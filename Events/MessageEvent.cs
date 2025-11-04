using DSharpPlus;
using DSharpPlus.EventArgs;

namespace DiscordBot.Events
{
    public static class MessageEvent
    {

        private static readonly HashSet<string> BadWords = new()
        {
            "Job",
            "Internship"
        };
        public static async Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
        {
            if (e.Author.IsBot) return;

            // Log mentions
            if (e.MentionedUsers.Any(u => u.Id == sender.CurrentUser.Id))
            {
                Console.WriteLine($"Mentioned by {e.Author.Username} in #{e.Channel.Name}");
            }

            if (ContainsBadWord(e.Message.Content))
            {
                try
                {
                    await e.Message.DeleteAsync("Cannot Use Such Words Here");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        private static bool ContainsBadWord(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return false;

            // First replace number/symbol substitutions with letters
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

            // Then remove ALL special characters and spaces
            cleanedContent = new string(cleanedContent.Where(c => char.IsLetterOrDigit(c)).ToArray());

            return BadWords.Any(badWord => cleanedContent.Contains(badWord.ToLower()));
        }

    }
}