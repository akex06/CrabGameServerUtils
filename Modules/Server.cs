using Il2CppSystem.Collections.Generic;

namespace TestPlugin.Modules;

public class Server
{
    public enum LeaveCode
    {
        ServerClosed = 0,
        Kicked = 1,
        Banned = 2,
        LostConnection = 3
    }

    public enum LobbyState
    {
        Menu = 0,
        Loading = 1,
        PreGame = 2,
        Started = 3,
        GameOver = 4
    }
    public static bool IsAdmin(ulong player)
    {
        return (ulong) SteamManager.Instance.field_Private_CSteamID_1 == player;
    }

    public static PlayerManager GetPlayerManagerByID(ulong id)
    {
        foreach (KeyValuePair<ulong,MonoBehaviourPublicCSstReshTrheObplBojuUnique> playerInfo in GameManager.Instance.activePlayers)
        {
            if (playerInfo.Key == id)
                return playerInfo.Value;
        }
        return null;
    }
    
    public static void SendMessage(ulong __0, string __1)
    {
        Packet packet = new Packet(2);
        packet.Method_Public_Void_UInt64_0(__0);
        packet.Method_Public_Void_String_0("Server");
        packet.Method_Public_Void_String_0(__1);
        ServerSend.Method_Private_Static_Void_UInt64_ObjectPublicIDisposableLi1ByInByBoUnique_0(__0, packet);
    }

    public static void Broadcast(string message)
    {
        foreach (var playerInfo in GameManager.Instance.activePlayers)
        {
            SendMessage(playerInfo.Value.steamProfile.m_SteamID, message);
        }
    }

    public static void Kick(ulong id)
    {
        ServerSend.LobbyClosed(id, (MonoBehaviourPublicCSDi2UIInstObUIloDiUnique.EnumNPublicSealedvaSeKiBaLo5vUnique)LeaveCode.Kicked);
    }
    
    public static void Ban(ulong id)
    {
        ServerSend.LobbyClosed(id, (MonoBehaviourPublicCSDi2UIInstObUIloDiUnique.EnumNPublicSealedvaSeKiBaLo5vUnique)LeaveCode.Banned);
    }

    public static Permission GetPermission(ulong id)
    {
        return Server.IsAdmin(id) ? Permission.Admin : Permission.User;
    }
}