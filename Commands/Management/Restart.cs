using TestPlugin.Modules;
using TestPlugin.Modules.Commands;

namespace TestPlugin.commands.Management;

public class Restart
{
    [Command("restart", permission: Permission.Admin)]
    public static void RestartCommand(CommandContext ctx)
    {
        LobbyManager.Instance.StartLobby();
        ctx.Send("Restarting lobby...");
    }
}