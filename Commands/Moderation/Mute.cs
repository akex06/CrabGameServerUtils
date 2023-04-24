using TestPlugin.Modules;
using TestPlugin.Modules.Commands;
using TestPlugin.Modules.Exceptions;

namespace TestPlugin.Commands.Moderation;

public class Mute
{
    [Command("mute", permission: Permission.Admin)]
    public static void MuteCommand(CommandContext ctx)
    {
        if (ctx.Parameters.Length == 0)
        {
            ctx.Send("Correct usage: /mute <player number>");
            return;
        }

        try
        {
            Player player = new Player(ctx.Parameters[0]);
            if (Plugin.MutedPlayers.Contains(player.ID))
            {
                ctx.Send("Player is already muted");
                return;
            }
            
            player.Mute();
            Plugin.MutedPlayers.Add(player.ID);
            ctx.Send($"Muted applied successfully");
        }
        catch (PlayerNotFoundError e)
        {
            ctx.Send(e.Message);
        }
    }
}