namespace TestPlugin.Modules.Commands;

public class CommandContext
{
    public Player Player;
    public string[] Parameters;

    public CommandContext(Player player, string[] parameters)
    {
        Player = player;
        Parameters = parameters;
    }
    
    public void Send(string message)
    {
        Server.SendMessage(Player.ID, message);
    }
}