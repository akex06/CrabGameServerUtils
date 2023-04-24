using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestPlugin.Modules.Commands;
using TestPlugin.Modules.Exceptions;
using UnityEngine;

namespace TestPlugin.Modules;

public class Mutes
{
    public static string mutedFile = Application.dataPath + "\\..\\Mutes.txt";

    [Command("mutes", permission: Permission.Admin)]
    public static void MutesCommand(CommandContext ctx)
    {
        ctx.Send(String.Join(" ", Plugin.MutedPlayers));
    }
    public static void LoadMutes()
    {
        if (!File.Exists(mutedFile))
        {
            File.Create(mutedFile).Close();
        }

        HashSet<ulong> mutes = new();
        foreach (string sID in File.ReadAllLines(mutedFile))
        {
            mutes.Append(ulong.Parse(sID));
        }

        Plugin.MutedPlayers = File.ReadAllLines(mutedFile).Select(id => ulong.Parse(id)).ToHashSet();
    }

    public static void Save()
    {
        File.WriteAllText(mutedFile, String.Join("\n", Plugin.MutedPlayers));
    }
}