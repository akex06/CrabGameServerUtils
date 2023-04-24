using System;

namespace TestPlugin.Modules.Commands;

[AttributeUsage(AttributeTargets.Method)]
public class CommandAttribute: Attribute
{
    public string Name;
    public Permission Permission;

    public CommandAttribute(
        string name,
        Permission permission = Permission.User
    )
    {
        Name = name;
        Permission = permission;
    }
}