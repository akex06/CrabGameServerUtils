using TestPlugin.Modules;
using TestPlugin.Modules.Commands;
using TestPlugin.Modules.Exceptions;

namespace TestPlugin.commands.Moderation;

public class Ban
{
    [Command("ban", permission: Permission.Admin)]
    public static void BanCommand(CommandContext ctx)
    {
        if (ctx.Parameters.Length == 0)
        {
            ctx.Send("Correct usage: /ban <player number>");
            return;
        }

        try
        {
            Player player = new Player(ctx.Parameters[0]);
            player.Ban();
        }
        catch (PlayerNotFoundError e)
        {
            ctx.Send(e.Message);
        }
    }
}