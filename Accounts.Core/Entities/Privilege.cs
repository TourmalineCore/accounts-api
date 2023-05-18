using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Accounts.Core.Entities;

public enum PrivilegesNames
{
    CanManageEmployees = 1,
    CanViewAnalytic,
    CanViewFinanceForPayroll
}

public static class PrivilegeNames
{
    public const string CanManageEmployees = "CanManageEmployees";
    public const string CanViewAnalytic = "CanViewAnalytic";
    public const string CanViewFinanceForPayroll = "CanViewFinanceForPayroll";

    public static bool IsAvailablePrivilegeName(string privilegeName)
    {
        var privilegeNames = GetFieldNames();
        return privilegeNames.Contains(privilegeName);
    }

    private static IEnumerable<string> GetFieldNames()
    {
        return typeof(PrivilegeNames)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(x => x.Name);
    }
}

public class Privilege : IIdentityEntity
{
    public long Id { get; private set; }

    public string Name { get; private set; }

    public List<Role> Roles { get; private set; }

    public Privilege(long id, string name)
    {
        if (!PrivilegeNames.IsAvailablePrivilegeName(name))
        {
            throw new ArgumentException("Incorrect privilege name");
        }

        Id = id;
        Name = name;
    }

    // To Db Context
    private Privilege()
    {
    }
}
