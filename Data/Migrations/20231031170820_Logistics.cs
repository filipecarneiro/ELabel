using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELabel.Data.Migrations
{
    /// <inheritdoc />
    public partial class Logistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Product",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "Product",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "Product");
        }
    }
}
