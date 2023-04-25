#nullable enable

using System.Collections.Generic;
using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using TestPlugin.Modules;
using TestPlugin.Modules.Commands;

namespace TestPlugin;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public static Plugin Instance;
    public static HashSet<ulong>? MutedPlayers;
    
    public override void Load()
    {
        Instance = this;
        Mutes.LoadMutes();

        Modules.Commands.Commands.LoadCommands();
            
        Harmony.CreateAndPatchAll(typeof(Plugin));
        Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    [HarmonyPatch(typeof(ServerSend), nameof(ServerSend.SendChatMessage))]
    [HarmonyPrefix]
    public static bool ServerReceiveMessage(ulong __0, string __1)
    {
        Player player = new Player(__0);
        if (__1.StartsWith("/"))
        {
            Modules.Commands.Commands.PerformCommand(player, __1);
            return false;
        }
        
        if (MutedPlayers != null && MutedPlayers.Contains(__0))
        {
            player.Send("You are muted");
            return false;
        }
        
        return true;
    }
}
