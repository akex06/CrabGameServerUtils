using TestPlugin.Modules;
using TestPlugin.Modules.Commands;
using TestPlugin.Modules.Exceptions;

namespace TestPlugin.Commands.Moderation;

public class UnMute
{
    [Command("unmute", permission: Permission.Admin)]
    public static void UnMuteCommand(CommandContext ctx)
    {
        if (ctx.Parameters.Length == 0)
        {
            ctx.Send("Correct usage: /unmute <player number>");
            return;
        }

        try
        {
            Player player = new Player(ctx.Parameters[0]);
            if (!Plugin.MutedPlayers.Contains(player.ID))
            {
                ctx.Send("Player is not muted");
                return;
            }

            player.UnMute();
            ctx.Send($"Unmuted successfully");
        }
        catch (PlayerNotFoundError e)
        {
            ctx.Send(e.Message);
        }
    }
}