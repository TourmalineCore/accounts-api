using System;

namespace Accounts.Core.Entities;

public class Permission
{
    public string Name { get; }

    public Permission(string name)
    {
        if (!Permissions.IsPermissionExists(name))
        {
            throw new ArgumentException($"Permission [{name}] doesn't exists");
        }

        Name = name;
    }
}