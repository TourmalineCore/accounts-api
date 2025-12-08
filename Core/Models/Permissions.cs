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
  public const string IsAccountsHardDeleteAllowed = "IsAccountsHardDeleteAllowed";
  public const string ViewRoles = "ViewRoles";
  public const string ManageRoles = "ManageRoles";
  public const string CanManageTenants = "CanManageTenants";
  public const string IsTenantsHardDeleteAllowed = "IsTenantsHardDeleteAllowed";

  public const string CanRequestCompensations = "CanRequestCompensations";
  public const string CanManageCompensations = "CanManageCompensations";
  public const string IsCompensationsHardDeleteAllowed = "IsCompensationsHardDeleteAllowed";

  public const string CanManageDocuments = "CanManageDocuments";

  public const string CanViewBooks = "CanViewBooks";
  public const string CanManageBooks = "CanManageBooks";
  public const string IsBooksHardDeleteAllowed = "IsBooksHardDeleteAllowed";

  public const string AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed = "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed";

  public const string AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed = "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed";
  public const string CanManageItemsTypes = "CanManageItemsTypes";
  public const string CanViewItemsTypes = "CanViewItemsTypes";

  public const string AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed = "AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed";
  public const string CanManageItems = "CanManageItems";
  public const string CanViewItems = "CanViewItems";

  public const string AUTO_TESTS_ONLY_IsWorkEntriesHardDeleteAllowed = "AUTO_TESTS_ONLY_IsWorkEntriesHardDeleteAllowed";
  public const string CanManagePersonalTimetracker = "CanManagePersonalTimetracker";
  
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
