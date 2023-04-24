using TestPlugin.Modules;
using TestPlugin.Modules.Commands;

namespace TestPlugin.commands;


public class Say
{
    [Command("say", permission: Permission.Admin)]
    public static void SayCommand(CommandContext ctx)
    {
        Server.Broadcast(System.String.Join(" ", ctx.Parameters));
    }
}