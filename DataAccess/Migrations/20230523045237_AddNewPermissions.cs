using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class AddNewPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "View personal profile", "Edit personal profile", "View contacts", "View salary and documents data", "Edit full employees data", "Access to analytical forecasts page", "View accounts", "Edit accounts", "View roles", "Edit roles" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "View personal profile", "Edit personal profile", "View contacts", "View salary and documents data", "Edit full employees data", "Access to analytical forecasts page", "View accounts", "Edit accounts", "View roles", "Edit roles" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CorporateEmail", "CreatedAt", "DeletedAtUtc", "FirstName", "LastName", "MiddleName" },
                values: new object[] { 2L, "inner-circle-admin@tourmalinecore.com", NodaTime.Instant.FromUnixTimeTicks(15778368000000000L), null, "Admin", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AccountRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { 2L, 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountRoles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 2L, 1L });

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Permissions",
                value: new[] { "CanManageEmployees" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Permissions",
                value: new[] { "CanManageEmployees", "CanViewAnalytic", "CanViewFinanceForPayroll" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Permissions" },
                values: new object[] { 3L, "Manager", new[] { "CanManageEmployees" } });
        }
    }
}
