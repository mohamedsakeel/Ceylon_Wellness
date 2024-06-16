using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeylonWellness.Web.Migrations
{
    /// <inheritdoc />
    public partial class added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dairy",
                table: "userHealthInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Eggs",
                table: "userHealthInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Hip",
                table: "userHealthInfos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MacroPreference",
                table: "userHealthInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Neck",
                table: "userHealthInfos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Waist",
                table: "userHealthInfos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "macropref",
                table: "userHealthInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mealintakepref",
                table: "userHealthInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "wheyproteinpref",
                table: "userHealthInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dairy",
                table: "userHealthInfos");

            migrationBuilder.DropColumn(
                name: "Eggs",
                table: "userHealthInfos");

            migrationBuilder.DropColumn(
                name: "Hip",
                table: "userHealthInfos");

            migrationBuilder.DropColumn(
                name: "MacroPreference",
                table: "userHealthInfos");

            migrationBuilder.DropColumn(
                name: "Neck",
                table: "userHealthInfos");

            migrationBuilder.DropColumn(
                name: "Waist",
                table: "userHealthInfos");

            migrationBuilder.DropColumn(
                name: "macropref",
                table: "userHealthInfos");

            migrationBuilder.DropColumn(
                name: "mealintakepref",
                table: "userHealthInfos");

            migrationBuilder.DropColumn(
                name: "wheyproteinpref",
                table: "userHealthInfos");
        }
    }
}
