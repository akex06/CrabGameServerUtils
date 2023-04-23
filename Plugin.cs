#nullable enable
using System;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;

namespace TestPlugin
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        public static Plugin Instance;
        public override void Load()
        {
            Instance = this;
            Commands.LoadCommands();
            
            foreach (System.Collections.Generic.KeyValuePair<string, MethodInfo> command in Commands.ParsedCommands)
            {
                Log.LogInfo($"{command.Key}: {command.Value}");
            }
            Harmony.CreateAndPatchAll(typeof(Plugin));
            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        [HarmonyPatch(typeof(ServerSend), nameof(ServerSend.SendChatMessage))]
        [HarmonyPrefix]
        public static bool ServerReceiveMessage(ulong __0, string __1)
        {
            if (!__1.StartsWith("/")) return true;

            Commands.PerformCommand(__0, __1);
            return false;
        }
    }
}
