using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Take100.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.Id);
                });

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
                    GramsOfFat = table.Column<float>(type: "real", nullable: false),
                    FoodCategoryId = table.Column<int>(type: "int", nullable: false)
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
                    HeightInInches = table.Column<float>(type: "real", nullable: false),
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
                    ServingsToEat = table.Column<float>(type: "real", nullable: false)
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

            migrationBuilder.InsertData(
                table: "FoodCategories",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "breakfast" },
                    { 2, "lunch" },
                    { 3, "snack" },
                    { 4, "dinner" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Calories", "FoodCategoryId", "GramsOfCarbs", "GramsOfFat", "GramsOfProtein", "Name", "Notes", "ServingSize" },
                values: new object[,]
                {
                    { 1, 105f, 3, 27f, 0.4f, 1.3f, "Banana", null, "118g" },
                    { 2, 120f, 3, 4f, 1f, 24f, "Protein Shake", "Optimum Nutrition - Vanilla Ice Cream", "118g" },
                    { 3, 265f, 1, 57.5f, 2.8f, 9.5f, "Frosted Miniwheats", "with 120mL milk", "60g" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Goal", "HeightInInches", "HipCircumferenceInches", "HoursExercisePerWeek", "IsMale", "Name", "NeckCircumferenceInches", "WaistCircumferenceInches", "WeightInLbs" },
                values: new object[,]
                {
                    { 1, 37, 0, 69f, 0f, 3, true, "John", 13f, 32f, 145f },
                    { 2, 54, 0, 65f, 38f, 1, false, "Mary", 15f, 38f, 170f }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "DayNumber", "DietUserId", "FoodId", "ServingsToEat" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1f },
                    { 2, 1, 1, 2, 2f },
                    { 3, 2, 2, 3, 1f },
                    { 4, 2, 2, 2, 0.5f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DietUserId",
                table: "Meals",
                column: "DietUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_FoodId",
                table: "Meals",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodCategories");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
