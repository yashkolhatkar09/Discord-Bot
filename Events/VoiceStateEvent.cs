using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;

namespace DiscordBot.Events
{
    public static class VoiceStateEvent
    {
        //public static async Task OnVoiceStateUpdated(DiscordClient sender, VoiceStateUpdateEventArgs e)
        //{
        //    // User joined a voice channel
        //    if (e.Before?.Channel == null && e.After?.Channel != null)
        //    {
        //        var textChannel = e.Guild.Channels.Values
        //            .FirstOrDefault(c => c.Type == ChannelType.Text &&
        //                               c.PermissionsFor(e.Guild.CurrentMember)
        //                                .HasPermission(Permissions.SendMessages));

        //        if (textChannel != null)
        //        {
        //            await textChannel.SendMessageAsync(
        //                $"👋 Hey {e.User.Mention}, welcome to **{e.After.Channel.Name}**!"
        //            );
        //        }
        //    }

        //    // User left a voice channel
        //    if (e.Before?.Channel != null && e.After?.Channel == null)
        //    {
        //        // Optional: Handle user leaving
        //    }
        //}
    }
}