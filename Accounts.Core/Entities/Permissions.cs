using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Accounts.Core.Entities;

public static class Permissions
{
    public const string ViewPersonalProfile = "View personal profile";
    public const string EditPersonalProfile = "Edit personal profile";
    public const string ViewContacts = "View contacts";
    public const string ViewSalaryAndDocumentsData = "View salary and documents data";
    public const string EditFullEmployeesData = "Edit full employees data";
    public const string AccessAnalyticalForecastsPage = "Access to analytical forecasts page";
    public const string ViewAccounts = "View accounts";
    public const string EditAccounts = "Edit accounts";
    public const string ViewRoles = "View roles";
    public const string EditRoles = "Edit roles";

    public static bool IsPermissionExists(string permissionName)
    {
        var permissionNames = GetPermissionNames();
        return permissionNames.Contains(permissionName);
    }

    private static IEnumerable<string?> GetPermissionNames()
    {
        return typeof(Permissions)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(x => x.GetValue(null)?.ToString());
    }
}