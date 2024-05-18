using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeylonWellness.Web.Migrations
{
    /// <inheritdoc />
    public partial class dd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionQuantity_Meals_MealId",
                table: "NutritionQuantity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionQuantity",
                table: "NutritionQuantity");

            migrationBuilder.RenameTable(
                name: "NutritionQuantity",
                newName: "NutritionQuantities");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionQuantity_MealId",
                table: "NutritionQuantities",
                newName: "IX_NutritionQuantities_MealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionQuantities",
                table: "NutritionQuantities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RecipeMeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMeals", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionQuantities_Meals_MealId",
                table: "NutritionQuantities",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionQuantities_Meals_MealId",
                table: "NutritionQuantities");

            migrationBuilder.DropTable(
                name: "RecipeMeals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionQuantities",
                table: "NutritionQuantities");

            migrationBuilder.RenameTable(
                name: "NutritionQuantities",
                newName: "NutritionQuantity");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionQuantities_MealId",
                table: "NutritionQuantity",
                newName: "IX_NutritionQuantity_MealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionQuantity",
                table: "NutritionQuantity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionQuantity_Meals_MealId",
                table: "NutritionQuantity",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
