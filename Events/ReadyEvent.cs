using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace DiscordBot.Events
{
    public static class ReadyEvent
    {
        public static Task OnReady(DiscordClient sender, ReadyEventArgs e)
        {
            Console.WriteLine($"{sender.CurrentUser.Username} is online!");
            Console.WriteLine($"Connected to {sender.Guilds.Count} servers");
            return Task.CompletedTask;
        }
    }
}