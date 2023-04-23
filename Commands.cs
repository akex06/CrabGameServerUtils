using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestPlugin;

public class Commands
{
    public static readonly System.Collections.Generic.Dictionary<string, MethodInfo> ParsedCommands = new();

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
        }
    }

    public static void PerformCommand(ulong player, string message)
    {
        List<string> parameters = new List<string>(message.Split().Skip(1));
        string name = message.Split()[0].Substring(1);

        if (!ParsedCommands.ContainsKey(name))
        {
            Server.SendMessage(player, "That command does not exist, please use /help for a list of all commands");
            return;
        }
        
        MethodInfo methodInfo = ParsedCommands[name];
        methodInfo.Invoke(null, new object[] {player, name, parameters});
    }
}