using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Accounts.Core.Entities;

public static class Permissions
{
    public const string CanManageEmployees = "CanManageEmployees";
    public const string CanViewAnalytic = "CanViewAnalytic";
    public const string CanViewFinanceForPayroll = "CanViewFinanceForPayroll";

    public static bool IsPermissionExists(string permissionName)
    {
        var permissionNames = GetPermissionNames();
        return permissionNames.Contains(permissionName);
    }

    private static IEnumerable<string> GetPermissionNames()
    {
        return typeof(Permissions)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(x => x.Name);
    }
}