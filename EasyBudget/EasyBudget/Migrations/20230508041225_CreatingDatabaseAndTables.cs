using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyBudget.Migrations
{
    /// <inheritdoc />
    public partial class CreatingDatabaseAndTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movements_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "Name", "Type", "Updated" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2480), "Transport", 2, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2494) },
                    { 2L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2495), "Food", 2, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2496) },
                    { 3L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2496), "Groceries", 2, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2497) },
                    { 4L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2497), "Health", 2, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2498) },
                    { 5L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2498), "Work", 2, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2499) },
                    { 6L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2499), "Home", 2, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2500) },
                    { 7L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2500), "Investments", 2, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2501) },
                    { 8L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2501), "Others expenses", 2, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2502) },
                    { 9L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2502), "Salary", 1, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2503) },
                    { 10L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2503), "Investments", 1, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2504) },
                    { 11L, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2505), "Other incomes", 1, new DateTime(2023, 5, 8, 1, 12, 25, 122, DateTimeKind.Local).AddTicks(2505) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movements_CategoryId",
                table: "Movements",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movements");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
