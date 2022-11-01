using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class AddDefaultPrivilegesToRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "CanViewEmployeePage");

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "PrivilegesId", "RolesId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 1L, 2L },
                    { 2L, 3L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegesId", "RolesId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegesId", "RolesId" },
                keyValues: new object[] { 1L, 2L });

            migrationBuilder.DeleteData(
                table: "RolePrivileges",
                keyColumns: new[] { "PrivilegesId", "RolesId" },
                keyValues: new object[] { 2L, 3L });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "CanViewEmployeeList");

            migrationBuilder.InsertData(
                table: "Privilege",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3L, "CanViewEmployeePage" });
        }
    }
}
