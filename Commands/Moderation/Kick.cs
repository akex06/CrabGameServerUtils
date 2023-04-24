using TestPlugin.Modules;
using TestPlugin.Modules.Commands;
using TestPlugin.Modules.Exceptions;

namespace TestPlugin.commands.Moderation;

public class Kick
{
    [Command("kick", Permission.Admin)]
    public static void KickCommand(CommandContext ctx)
    {
        if (ctx.Parameters.Length == 0)
        {
            ctx.Send("Correct usage: /kick <playerNumber>");
            return;
        }

        try
        {
            Player kickedPlayer = new Player(ctx.Parameters[0]);
            kickedPlayer.Kick();
        }
        catch (PlayerNotFoundError e)
        {
            ctx.Send(e.Message);
        }

    }
}