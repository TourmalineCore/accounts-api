using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class AddCeoAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CorporateEmail", "CreatedAt", "DeletedAtUtc", "FirstName", "LastName" },
                values: new object[] { 1L, "ceo@tourmalinecore.com", NodaTime.Instant.FromUnixTimeTicks(15778368000000000L), null, "Ceo", "Ceo" });

            migrationBuilder.InsertData(
                table: "AccountRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { 1L, 2L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountRoles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1L, 2L });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
