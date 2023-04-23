# CrabGameServerUtils

# Examples
## Command Example
```cs
public class Say
{
    [Command("say")]    
    public static bool SayCommand(ulong executor, string command, List<string> parameters)
    {
        if (!Server.IsAdmin(executor))
        {
            Server.SendMessage(executor, "You are not the lobby owner");
            return false;
        }
        
        Server.Broadcast(String.Join(" ", parameters));
        return true;
    }
}
```
