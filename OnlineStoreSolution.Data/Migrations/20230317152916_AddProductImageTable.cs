using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 3, 17, 0, 40, 51, 410, DateTimeKind.Local).AddTicks(6245));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 101,
                column: "ConcurrencyStamp",
                value: "d0b52b7d-698b-4548-9f36-5ec8657d3e74");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7036bcb5-96d1-4901-a295-c5f88e5d8c3e", "AQAAAAIAAYagAAAAEI5DjGfaIqGzdHIvNr7WRSZirNzIPAKWHKeTkjPa0CJltDWw/7X0ifXeb02d3eE0OA==" });
        }
    }
}
