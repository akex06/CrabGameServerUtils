using System;
using System.IO;
using System.Linq;
using Il2CppSystem.Collections.Generic;
using UnityEngine;

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
    
    public static void SendMessageBy(ulong sender, ulong receiver, string senderName, string message)
    {
        Packet packet = new Packet(2);
        packet.Method_Public_Void_UInt64_0(sender);
        packet.Method_Public_Void_String_0(senderName);
        packet.Method_Public_Void_String_0(message);
        ServerSend.Method_Private_Static_Void_UInt64_ObjectPublicIDisposableLi1ByInByBoUnique_0(receiver, packet);
    }

    public static void SendMessage(ulong __0, string __1)
    {
        Packet packet = new Packet(2);
        packet.Method_Public_Void_UInt64_0(1UL);
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
        LobbyManager.Instance.BanPlayer(id);
    }

    public static Permission GetPermission(ulong id)
    {
        return Server.IsAdmin(id) ? Permission.Admin : Permission.User;
    }
}