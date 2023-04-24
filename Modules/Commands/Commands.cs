using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestPlugin.Modules.Commands;

public class Commands
{
    public static Dictionary<string, MethodInfo> ParsedCommands = new();

    public static void LoadCommands()
    {
        foreach (MethodInfo methodInfo in Assembly.GetExecutingAssembly().GetTypes()
                     .Select(type => type.GetMethods()
                         .FirstOrDefault(method => method.GetCustomAttribute(typeof(CommandAttribute)) != null)))
        {
            if (methodInfo == null)
                continue;

            CommandAttribute attribute = methodInfo.GetCustomAttribute<CommandAttribute>();
            if (attribute == null)
                continue;
            
            ParsedCommands.Add(attribute.Name, methodInfo);
            Plugin.Instance.Log.LogInfo($"Loaded \"{attribute.Name}\" command");
        }
        
        Plugin.Instance.Log.LogInfo($"Loaded {ParsedCommands.Count} commands");
    }

    public static void PerformCommand(ulong id, string message)
    {
        string[] separated = message.Split(" ");
        string[] parameters = separated[1..];
        string name = separated[0][1..];

        Player player = new Player(id);

        if (!ParsedCommands.ContainsKey(name))
        {
            player.Send(Messages.Messages.CommandDoesNotExist);
            return;
        }

        MethodInfo methodInfo = ParsedCommands[name];
        CommandAttribute attribute = methodInfo.GetCustomAttribute<CommandAttribute>();

        if (player.IsAdmin() || (attribute.Permission == Permission.User))
        {
            methodInfo.Invoke(null, new object[] { new CommandContext(player, parameters) });
        }
        else
        {
            player.Send(Messages.Messages.MissingPermissions);
        }
    }
}