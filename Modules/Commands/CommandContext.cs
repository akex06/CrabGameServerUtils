namespace TestPlugin.Modules.Commands;

public class CommandContext
{
    public Player Author;
    public string[] Parameters;

    public CommandContext(Player player, string[] parameters)
    {
        Author = player;
        Parameters = parameters;
    }
    
    public void Send(string message)
    {
        Server.SendMessage(Author.ID, message);
    }
}