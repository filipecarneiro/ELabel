using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELabel.Data.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Volume = table.Column<float>(type: "real", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    WineVintage = table.Column<int>(type: "int", nullable: true),
                    WineType = table.Column<int>(type: "int", nullable: true),
                    WineStyle = table.Column<int>(type: "int", nullable: true),
                    WineAppellation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ean = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name_Volume_WineVintage",
                table: "Product",
                columns: new[] { "Name", "Volume", "WineVintage" },
                unique: true,
                filter: "[Volume] IS NOT NULL AND [WineVintage] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
