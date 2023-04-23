namespace TestPlugin;

public class Server
{
    public static bool IsAdmin(ulong player)
    {
        return (ulong) SteamManager.Instance.field_Private_CSteamID_1 == player;
    }
    
    public static void SendMessage(ulong __0, string __1)
    {
        Plugin.Instance.Log.LogInfo("Sendmessage");
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
}