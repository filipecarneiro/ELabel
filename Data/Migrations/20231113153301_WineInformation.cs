using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELabel.Data.Migrations
{
    /// <inheritdoc />
    public partial class WineInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Name_Volume_WineVintage",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Product_Name_Volume_WineVintage",
                table: "Product",
                columns: new[] { "Name", "Volume", "WineVintage" },
                unique: true,
                filter: "[Volume] IS NOT NULL AND [WineVintage] IS NOT NULL");
        }
    }
}
