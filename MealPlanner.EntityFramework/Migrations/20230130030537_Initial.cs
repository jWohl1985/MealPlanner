using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealPlanner.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServingSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<float>(type: "real", nullable: false),
                    GramsOfProtein = table.Column<float>(type: "real", nullable: false),
                    GramsOfCarbs = table.Column<float>(type: "real", nullable: false),
                    GramsOfFat = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    IsMale = table.Column<bool>(type: "bit", nullable: false),
                    WeightInLbs = table.Column<float>(type: "real", nullable: false),
                    HeightFeetComponent = table.Column<int>(type: "int", nullable: false),
                    HeightInchComponent = table.Column<int>(type: "int", nullable: false),
                    HoursExercisePerWeek = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    NeckCircumferenceInches = table.Column<float>(type: "real", nullable: false),
                    WaistCircumferenceInches = table.Column<float>(type: "real", nullable: false),
                    HipCircumferenceInches = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DietUserId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    ServingsToEat = table.Column<float>(type: "real", nullable: false),
                    HasBeenEaten = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_Users_DietUserId",
                        column: x => x.DietUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DietUserId = table.Column<int>(type: "int", nullable: false),
                    WeightInLbs = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightLogs_Users_DietUserId",
                        column: x => x.DietUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Calories", "GramsOfCarbs", "GramsOfFat", "GramsOfProtein", "Name", "Notes", "ServingSize" },
                values: new object[,]
                {
                    { 1, 105f, 27f, 0.4f, 1.3f, "Banana", null, "118g" },
                    { 2, 120f, 4f, 1f, 24f, "Protein Shake", "Optimum Nutrition - Vanilla Ice Cream", "118g" },
                    { 3, 265f, 57.5f, 2.8f, 9.5f, "Frosted Miniwheats", "with 120mL milk", "60g" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DietUserId",
                table: "Meals",
                column: "DietUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_FoodId",
                table: "Meals",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightLogs_DietUserId",
                table: "WeightLogs",
                column: "DietUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "WeightLogs");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
