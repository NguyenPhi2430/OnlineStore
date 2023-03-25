using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 3, 17, 0, 40, 51, 410, DateTimeKind.Local).AddTicks(6245));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Desc", "Name", "NormalizedName" },
                values: new object[] { 101, "d0b52b7d-698b-4548-9f36-5ec8657d3e74", "Adminnistrator", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 101, 101 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "dateOfBirth", "firstName", "lastName" },
                values: new object[] { 101, 0, "7036bcb5-96d1-4901-a295-c5f88e5d8c3e", "admin@gmail.com", true, false, null, "admin@gmail.com", "admin", "AQAAAAIAAYagAAAAEI5DjGfaIqGzdHIvNr7WRSZirNzIPAKWHKeTkjPa0CJltDWw/7X0ifXeb02d3eE0OA==", null, false, "", false, "admin", new DateTime(1996, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen", "Phi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 101, 101 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 3, 17, 0, 27, 58, 591, DateTimeKind.Local).AddTicks(4478));
        }
    }
}
