using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class AddActualRolesAndPrivileges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegesId", "RolesId" },
                keyValues: new object[] { 2L, 3L });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "CanManageEmployees");

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "CanViewAnalytic");

            migrationBuilder.InsertData(
                table: "Privilege",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3L, "CanViewFinanceForPayroll" });

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegesId", "RolesId" },
                values: new object[,]
                {
                    { 1L, 3L },
                    { 2L, 2L }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "CEO", "CEO" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Manager", "Manager" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[] { 4L, "Employee", "Employee" });

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegesId", "RolesId" },
                values: new object[] { 3L, 2L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegesId", "RolesId" },
                keyValues: new object[] { 1L, 3L });

            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegesId", "RolesId" },
                keyValues: new object[] { 2L, 2L });

            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegesId", "RolesId" },
                keyValues: new object[] { 3L, 2L });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "CanManageEverything");

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "CanViewEmployeePage");

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegesId", "RolesId" },
                values: new object[] { 2L, 3L });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Seo", "Seo" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Employee", "Employee" });
        }
    }
}