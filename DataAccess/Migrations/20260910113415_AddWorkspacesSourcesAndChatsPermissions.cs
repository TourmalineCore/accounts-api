using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class AddWorkspacesSourcesAndChatsPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed", "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed", "CanManageItemsTypes", "CanViewItemsTypes", "AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed", "CanManageItems", "CanViewItems", "AUTO_TESTS_ONLY_IsEntriesHardDeleteAllowed", "CanManagePersonalTimeTracker", "CanViewAllTimeTrackerEntries", "CanViewAllProjects", "CanViewPersonalReport", "CanViewInvoices", "CanViewAllWorkspaces", "CanViewAllSources", "CanManageSources", "CanForceSyncSources", "CanInteractWithChats", "AUTO_TESTS_ONLY_IsWorkspacesAddAllowed", "AUTO_TESTS_ONLY_IsWorkspacesHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSourcesHardDeleteAllowed", "AUTO_TESTS_ONLY_IsChatsSendMessageForEvaluationAllowed", "AUTO_TESTS_ONLY_IsChatFeedbackGetAllowed", "AUTO_TESTS_ONLY_IsChatsHardDeleteAllowed" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed", "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed", "CanManageItemsTypes", "CanViewItemsTypes", "AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed", "CanManageItems", "CanViewItems", "AUTO_TESTS_ONLY_IsEntriesHardDeleteAllowed", "CanManagePersonalTimeTracker", "CanViewAllTimeTrackerEntries", "CanViewAllProjects", "CanViewPersonalReport", "CanViewInvoices", "CanViewAllWorkspaces", "CanViewAllSources", "CanManageSources", "CanForceSyncSources", "CanInteractWithChats", "AUTO_TESTS_ONLY_IsWorkspacesAddAllowed", "AUTO_TESTS_ONLY_IsWorkspacesHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSourcesHardDeleteAllowed", "AUTO_TESTS_ONLY_IsChatsSendMessageForEvaluationAllowed", "AUTO_TESTS_ONLY_IsChatFeedbackGetAllowed", "AUTO_TESTS_ONLY_IsChatsHardDeleteAllowed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed", "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed", "CanManageItemsTypes", "CanViewItemsTypes", "AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed", "CanManageItems", "CanViewItems", "AUTO_TESTS_ONLY_IsEntriesHardDeleteAllowed", "CanManagePersonalTimeTracker", "CanViewAllTimeTrackerEntries", "CanViewAllProjects", "CanViewPersonalReport", "CanViewInvoices" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "AUTO_TESTS_ONLY_IsSetUserPasswordBypassingEmailConfirmationAllowed", "AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed", "CanManageItemsTypes", "CanViewItemsTypes", "AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed", "CanManageItems", "CanViewItems", "AUTO_TESTS_ONLY_IsEntriesHardDeleteAllowed", "CanManagePersonalTimeTracker", "CanViewAllTimeTrackerEntries", "CanViewAllProjects", "CanViewPersonalReport", "CanViewInvoices" });
        }
    }
}
