using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class AddAccountRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Accounts",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "CorporateEmail",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAt",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoles_Users_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CorporateEmail",
                table: "Accounts",
                column: "CorporateEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_RoleId",
                table: "AccountRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropIndex(
                name: "IX_Users_CorporateEmail",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CorporateEmail",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Accounts",
                newName: "Email");

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Accounts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
