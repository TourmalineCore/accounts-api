using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class AddDocumentsPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations", "CanManageDocuments" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "ViewPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "ManageAccounts", "ViewRoles", "ManageRoles", "CanRequestCompensations", "CanManageCompensations" });
        }
    }
}