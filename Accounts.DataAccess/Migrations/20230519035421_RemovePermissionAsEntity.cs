using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class RemovePermissionAsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "Roles");

            migrationBuilder.AddColumn<string[]>(
                name: "Permissions",
                table: "Roles",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

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

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Permissions",
                value: new[] { "CanManageEmployees" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "CanManageEmployees" },
                    { 2L, "CanViewAnalytic" },
                    { 3L, "CanViewFinanceForPayroll" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NormalizedName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "NormalizedName",
                value: "CEO");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "NormalizedName",
                value: "Manager");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[] { 4L, "Employee", "Employee" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 1L, 2L },
                    { 1L, 3L },
                    { 2L, 2L },
                    { 3L, 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_RoleId",
                table: "RolesPermissions",
                column: "RoleId");
        }
    }
}
