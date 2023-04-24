using System.Collections.Generic;
using System.Linq;
using TestPlugin.Modules;
using TestPlugin.Modules.Exceptions;

namespace TestPlugin;

public class Player
{
    public ulong ID;
    public PlayerManager PlayerManager;
    public Permission Permission;

    public Player(ulong id)
    {
        ID = id;
        PlayerManager = Server.GetPlayerManagerByID(id);
        Permission = Server.GetPermission(id);
    }

    public Player(string playerNumber)
    {
        
        foreach (Il2CppSystem.Collections.Generic.KeyValuePair<ulong, MonoBehaviourPublicCSstReshTrheObplBojuUnique> playerInfo in GameManager.Instance.activePlayers)
        {
            if (playerInfo.Value.playerNumber.ToString().Equals(playerNumber))
            {
                ID = playerInfo.Value.steamProfile.m_SteamID;
                PlayerManager = playerInfo.Value;
                Permission = Server.GetPermission(ID);
                return;
            }
        }

        throw new PlayerNotFoundError($"Player number \"{playerNumber}\" is not a valid player number");
    }

    public override string ToString()
    {
        return $"Player(ID: {ID})";
    }

    public void Kick()
    {
        Server.Kick(ID);
    }
    
    public void Ban()
    {
        Server.Ban(ID);
    }

    public void Send(string message)
    {
        Server.SendMessage(ID, message);
    }

    public bool IsAdmin()
    {
        return Server.IsAdmin(ID);
    }

    public void Mute()
    {
        Mutes.Save();
        Plugin.MutedPlayers.Append(ID);
    }

    public void UnMute()
    {
        Mutes.Save();
        Plugin.MutedPlayers.Remove(ID);
    }
}