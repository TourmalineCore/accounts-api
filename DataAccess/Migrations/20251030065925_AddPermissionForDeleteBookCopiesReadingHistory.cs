﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class AddPermissionForDeleteBookCopiesReadingHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "IsBookCopiesHardDeleteAllowed", "IsBookCopiesReadingHistoryHardDeleteAllowed" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "IsBookCopiesHardDeleteAllowed", "IsBookCopiesReadingHistoryHardDeleteAllowed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "IsBookCopiesHardDeleteAllowed" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments", "CanManageTenants", "IsTenantsHardDeleteAllowed", "IsAccountsHardDeleteAllowed", "IsCompensationsHardDeleteAllowed", "CanViewBooks", "CanManageBooks", "IsBooksHardDeleteAllowed", "IsBookCopiesHardDeleteAllowed" });
        }
    }
}
