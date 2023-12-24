using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELabel.Data.Migrations
{
    /// <inheritdoc />
    public partial class Portability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalShortUrl",
                table: "Product",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RedirectUrl",
                table: "Product",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalShortUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "RedirectUrl",
                table: "Product");
        }
    }
}
