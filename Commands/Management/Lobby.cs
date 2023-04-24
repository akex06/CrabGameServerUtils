using System;
using System.Linq;
using TestPlugin.Modules;
using TestPlugin.Modules.Commands;

namespace TestPlugin.commands.Management;

public class Lobby
{
    public static readonly string[] phKeys = 
        { "Modes", "AllMaps", "Maps", "LobbyKeyId", "Voice Chat", "LobbyState", "LobbyName", "Version", "PlayersIn" };
    
    [Command("lobby", permission: Permission.Admin)]
    public static void LobbyCommand(CommandContext ctx)
    {
        if (ctx.Parameters.Length == 0)
        {
            ctx.Send("Correct usage: /lobby <property> [state]");
            return;
        }
        
        if (!phKeys.Contains(ctx.Parameters[0]))
        {
            ctx.Send($"Available properties: {String.Join(" ", phKeys)}");
            return;
        }

        if (ctx.Parameters.Length > 1)
        {
            SteamworksNative.SteamMatchmaking.SetLobbyData(
                SteamManager.Instance.currentLobby,
                ctx.Parameters[0],
                String.Join(" ", ctx.Parameters[1..])
            );
        }

        ctx.Send(
            SteamworksNative.SteamMatchmaking.GetLobbyData(
                SteamManager.Instance.currentLobby,
                ctx.Parameters[0]
            )
        );
    }
}