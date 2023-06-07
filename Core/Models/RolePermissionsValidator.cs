using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Models;

public static class RolePermissionsValidator
{
    private static readonly Dictionary<string, string[]> PermissionsWithDependencies = new()
    {
        { Permissions.EditPersonalProfile, new[] { Permissions.ViewPersonalProfile } },
        { Permissions.ViewSalaryAndDocumentsData, new[] { Permissions.ViewContacts } },
        { Permissions.EditFullEmployeesData, new[] { Permissions.ViewContacts, Permissions.ViewSalaryAndDocumentsData } },
        { Permissions.ManageAccounts, new[] { Permissions.ViewAccounts } },
        { Permissions.ManageRoles, new[] { Permissions.ViewRoles } },
    };

    public static void ValidatePermissions(IEnumerable<string> permissions)
    {
        foreach (var permission in permissions)
        {
            if (!PermissionsWithDependencies.ContainsKey(permission))
            {
                continue;
            }

            var permissionDependencies = PermissionsWithDependencies[permission];

            if (permissionDependencies.Any(permissionDependency => !permissions.Contains(permissionDependency)))
            {
                throw new ArgumentException($"Permission '{permission}' requires permissions [{string.Join(',', permissionDependencies)}]");
            }
        }
    }
}