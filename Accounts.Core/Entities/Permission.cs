using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Accounts.Core.Entities;

public static class PermissionNames
{
    public const string CanManageEmployees = "CanManageEmployees";
    public const string CanViewAnalytic = "CanViewAnalytic";
    public const string CanViewFinanceForPayroll = "CanViewFinanceForPayroll";

    public static bool IsAvailablePermissionName(string permissionName)
    {
        var permissionNames = GetFieldNames();
        return permissionNames.Contains(permissionName);
    }

    private static IEnumerable<string> GetFieldNames()
    {
        return typeof(PermissionNames)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(x => x.Name);
    }
}

public class Permission : IIdentityEntity
{
    public long Id { get; private set; }

    public string Name { get; private set; }

    public List<Role> Roles { get; private set; }

    public Permission(long id, string name)
    {
        if (!PermissionNames.IsAvailablePermissionName(name))
        {
            throw new ArgumentException("Incorrect permission name");
        }

        Id = id;
        Name = name;
    }

    // To Db Context
    private Permission()
    {
    }
}
