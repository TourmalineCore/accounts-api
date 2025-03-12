using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Models;

public static class Permissions
{
    public const string ViewPersonalProfile = "ViewPersonalProfile";
    public const string ViewContacts = "ViewContacts";
    public const string ViewSalaryAndDocumentsData = "ViewSalaryAndDocumentsData";
    public const string EditFullEmployeesData = "EditFullEmployeesData";
    public const string AccessAnalyticalForecastsPage = "AccessAnalyticalForecastsPage";
    public const string ViewAccounts = "ViewAccounts";
    public const string ManageAccounts = "ManageAccounts";
    public const string ViewRoles = "ViewRoles";
    public const string ManageRoles = "ManageRoles";
    public const string CanRequestCompensations = "CanRequestCompensations";
    public const string CanManageCompensations = "CanManageCompensations";
    public const string CanManageBooks = "CanManageBooks";
    public const string CanManageDocuments = "CanManageDocuments";
    public const string CanManageTenants = "CanManageTenants";
    public const string IsTenantsHardDeleteAllowed = "IsTenantsHardDeleteAllowed";
    public const string IsAccountsHardDeleteAllowed = "IsAccountsHardDeleteAllowed";
    public const string IsBooksHardDeleteAllowed = "IsBooksHardDeleteAllowed";

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