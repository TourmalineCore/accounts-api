using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    public partial class AddNewTrialAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CorporateEmail", "CreatedAt", "DeletedAtUtc", "FirstName", "IsBlocked", "LastName", "MiddleName", "TenantId" },
                values: new object[] { 3L, "trial@tourmalinecore.com", NodaTime.Instant.FromUnixTimeTicks(15778368000000000L), null, "Trial", false, "Trial", "Trial", 1L });

            migrationBuilder.InsertData(
                table: "AccountRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { 3L, 2L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountRoles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 3L, 2L });

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
