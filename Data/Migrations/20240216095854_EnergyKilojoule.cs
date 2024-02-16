using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELabel.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnergyKilojoule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "NutritionInformation_Energy_Kilojoule",
                table: "Product",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NutritionInformation_Energy_Kilojoule",
                table: "Product");
        }
    }
}
