using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterFileSizeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 3, 17, 23, 55, 38, 361, DateTimeKind.Local).AddTicks(9376));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 101,
                column: "ConcurrencyStamp",
                value: "a6049f6e-6e8f-4aca-b7c2-095c398eb4ab");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "580e4879-61d0-4127-928d-c8d3a19ef804", "AQAAAAIAAYagAAAAEF9z6b6u/fPbjXKKValMTiVHFaXt8MxG8Ai8PsfZJijkSgrplcdSeX4teTaBBBpKKQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 3, 17, 22, 29, 15, 793, DateTimeKind.Local).AddTicks(2968));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 101,
                column: "ConcurrencyStamp",
                value: "e1034981-7381-4bb9-84f2-95daf32f1b3e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0ae707f7-748d-4c4d-a8ce-e4dd6d7da4ef", "AQAAAAIAAYagAAAAEJGuMxyyabQ1PQ1sLM0vRezUXAMyMWYulxO99gV6UsSzwxX/jrkQ+NKXzYhDoG4S/g==" });
        }
    }
}
