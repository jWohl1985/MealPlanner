using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Take100.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class WeightLogs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WeightLogs",
                columns: new[] { "Id", "Date", "DietUserId", "WeightInLbs" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 160f },
                    { 2, new DateTime(2022, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 158f },
                    { 3, new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 180f },
                    { 4, new DateTime(2023, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 179.4f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeightLogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeightLogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeightLogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeightLogs",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
