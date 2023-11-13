using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELabel.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResponsibleConsumption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ResponsibleConsumption_WarningDrinkingBelowLegalAge",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ResponsibleConsumption_WarningDrinkingDuringPregnancy",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ResponsibleConsumption_WarningDrinkingWhenDriving",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleConsumption_WarningDrinkingBelowLegalAge",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ResponsibleConsumption_WarningDrinkingDuringPregnancy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ResponsibleConsumption_WarningDrinkingWhenDriving",
                table: "Product");
        }
    }
}
