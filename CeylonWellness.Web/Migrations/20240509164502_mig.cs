using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeylonWellness.Web.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "userHealthInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Meals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GoalId",
                table: "DietPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DietPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "NutritionQuantity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionQuantity_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_PlanId",
                table: "Meals",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPlans_GoalId",
                table: "DietPlans",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionQuantity_MealId",
                table: "NutritionQuantity",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlans_Goals_GoalId",
                table: "DietPlans",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_DietPlans_PlanId",
                table: "Meals",
                column: "PlanId",
                principalTable: "DietPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietPlans_Goals_GoalId",
                table: "DietPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_DietPlans_PlanId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "NutritionQuantity");

            migrationBuilder.DropIndex(
                name: "IX_Meals_PlanId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_DietPlans_GoalId",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "userHealthInfos");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "GoalId",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DietPlans");
        }
    }
}
