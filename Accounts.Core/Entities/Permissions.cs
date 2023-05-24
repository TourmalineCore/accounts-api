using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Accounts.Core.Entities;

public static class Permissions
{
    public const string ViewPersonalProfile = "ViewPersonalProfile";
    public const string EditPersonalProfile = "EditPersonalProfile";
    public const string ViewContacts = "ViewContacts";
    public const string ViewSalaryAndDocumentsData = "ViewSalaryAndDocumentsData";
    public const string EditFullEmployeesData = "EditFullEmployeesData";
    public const string AccessAnalyticalForecastsPage = "AccessAnalyticalForecastsPage";
    public const string ViewAccounts = "ViewAccounts";
    public const string ManageAccounts = "ManageAccounts";
    public const string ViewRoles = "ViewRoles";
    public const string ManageRoles = "ManageRoles";

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