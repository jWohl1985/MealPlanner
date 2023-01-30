using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Take100.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class HeightFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeightInInches",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "HeightFeetComponent",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HeightInchComponent",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HeightFeetComponent", "HeightInchComponent" },
                values: new object[] { 5, 9 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HeightFeetComponent", "HeightInchComponent" },
                values: new object[] { 5, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeightFeetComponent",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HeightInchComponent",
                table: "Users");

            migrationBuilder.AddColumn<float>(
                name: "HeightInInches",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HeightInInches",
                value: 69f);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HeightInInches",
                value: 65f);
        }
    }
}
