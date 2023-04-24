using System;

namespace TestPlugin.Modules.Exceptions;

[Serializable]
public class PlayerNotFoundError : Exception
{
    public PlayerNotFoundError() { }
    
    public PlayerNotFoundError(string message)
        : base(message) { }
}