using System;
using TestPlugin.Modules;
using TestPlugin.Modules.Commands;
using TestPlugin.Modules.Exceptions;

namespace TestPlugin.commands.General;

public class Msg
{
    [Command("msg")]
    public static void MessageCommand(CommandContext ctx)
    {
        if (ctx.Parameters.Length < 2)
        {
            ctx.Send("Correct usage: /msg <player> <message>");
            return;
        }

        try
        {
            Player player = new Player(ctx.Parameters[0]);
            
            Server.SendMessageBy(
                player.ID,
                ctx.Author.ID,
                $"[You -> {player.PlayerManager.username}]",
                String.Join(" ", ctx.Parameters[1..])
            );
            
            Server.SendMessageBy(
                ctx.Author.ID,
                player.ID,
                $"[{ctx.Author.PlayerManager.username} -> You]",
                String.Join(" ", ctx.Parameters[1..])
                );
        }
        catch (PlayerNotFoundError e)
        {
            ctx.Send(e.Message);
        }
    }
}