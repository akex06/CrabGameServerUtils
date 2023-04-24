using TestPlugin.Modules;
using TestPlugin.Modules.Commands;

namespace TestPlugin.commands.General;


public class Say
{
    [Command("say", permission: Permission.Admin)]
    public static void SayCommand(CommandContext ctx)
    {
        if (ctx.Parameters.Length == 0)
        {
            ctx.Send("Correct usage: /say <message>");
            return;
        }
        Server.Broadcast(System.String.Join(" ", ctx.Parameters));
    }
}