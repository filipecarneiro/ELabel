using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELabel.Data.Migrations
{
    /// <inheritdoc />
    public partial class FoodBusinessOperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FBOAddress",
                table: "Product",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FBOBusinessName",
                table: "Product",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FBOName",
                table: "Product",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FBOAddress",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FBOBusinessName",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FBOName",
                table: "Product");
        }
    }
}
