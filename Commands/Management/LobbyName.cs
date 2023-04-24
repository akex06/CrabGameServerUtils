using System;
using TestPlugin.Modules;
using TestPlugin.Modules.Commands;

namespace TestPlugin.commands.Management;

public class LobbyName
{
    [Command("lobbyname", permission: Permission.Admin)]
    public static void LobbyNameCommand(CommandContext ctx)
    {
        LobbyManager.Instance.gameSettings.field_Public_Int32_4 = 100;
        
        if (ctx.Parameters.Length != 0)
        {
            SteamworksNative.SteamMatchmaking.SetLobbyData(SteamManager.Instance.currentLobby, "LobbyName", String.Join(" ", ctx.Parameters));
        }

        ctx.Send(SteamworksNative.SteamMatchmaking.GetLobbyData(SteamManager.Instance.currentLobby, "LobbyName"));
    }
}