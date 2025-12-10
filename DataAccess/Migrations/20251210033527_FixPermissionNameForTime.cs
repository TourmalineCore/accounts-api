using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class FixPermissionNameForTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed", "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed", "CanManageItemsTypes", "CanViewItemsTypes", "AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed", "CanManageItems", "CanViewItems", "AUTO_TESTS_ONLY_IsWorkEntriesHardDeleteAllowed", "CanManagePersonalTimeTracker" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed", "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed", "CanManageItemsTypes", "CanViewItemsTypes", "AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed", "CanManageItems", "CanViewItems", "AUTO_TESTS_ONLY_IsWorkEntriesHardDeleteAllowed", "CanManagePersonalTimeTracker" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed", "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed", "CanManageItemsTypes", "CanViewItemsTypes", "AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed", "CanManageItems", "CanViewItems", "AUTO_TESTS_ONLY_IsWorkEntriesHardDeleteAllowed", "CanManagePersonalTimetracker" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed", "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed", "CanManageItemsTypes", "CanViewItemsTypes", "AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed", "CanManageItems", "CanViewItems", "AUTO_TESTS_ONLY_IsWorkEntriesHardDeleteAllowed", "CanManagePersonalTimetracker" });
        }
    }
}
