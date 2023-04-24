using System;

namespace TestPlugin.Modules;

[Flags]
public enum Permission
{
    User = 1,
    Admin = 2
}