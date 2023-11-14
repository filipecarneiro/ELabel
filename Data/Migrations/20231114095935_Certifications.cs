using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELabel.Data.Migrations
{
    /// <inheritdoc />
    public partial class Certifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Certifications_Organic",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certifications_Organic",
                table: "Product");
        }
    }
}
