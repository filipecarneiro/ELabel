using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELabel.Data.Migrations
{
    /// <inheritdoc />
    public partial class NutritionInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "NutritionInformation_CarbohydrateSugar",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "NutritionInformation_CarbohydrateTotal",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "NutritionInformation_Energy_Kilocalorie",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "NutritionInformation_FatSaturates",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "NutritionInformation_FatTotal",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "NutritionInformation_PortionVolume",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "NutritionInformation_Protein",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "NutritionInformation_Salt",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NutritionInformation_CarbohydrateSugar",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NutritionInformation_CarbohydrateTotal",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NutritionInformation_Energy_Kilocalorie",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NutritionInformation_FatSaturates",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NutritionInformation_FatTotal",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NutritionInformation_PortionVolume",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NutritionInformation_Protein",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NutritionInformation_Salt",
                table: "Product");
        }
    }
}
