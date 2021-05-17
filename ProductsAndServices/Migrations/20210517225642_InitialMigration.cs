using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAndServices.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductServices",
                columns: table => new
                {
                    ProductServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PriceAgreement = table.Column<bool>(type: "bit", nullable: false),
                    IsPriceChangeable = table.Column<bool>(type: "bit", nullable: false),
                    Exchangement = table.Column<bool>(type: "bit", nullable: false),
                    ExchangementCondition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServices", x => x.ProductServiceID);
                });

            migrationBuilder.CreateTable(
                name: "ProductServicePictures",
                columns: table => new
                {
                    PictureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductServiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServicePictures", x => x.PictureID);
                    table.ForeignKey(
                        name: "FK_ProductServicePictures_ProductServices_ProductServiceID",
                        column: x => x.ProductServiceID,
                        principalTable: "ProductServices",
                        principalColumn: "ProductServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductServicePrices",
                columns: table => new
                {
                    PriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    ProductServiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServicePrices", x => x.PriceID);
                    table.ForeignKey(
                        name: "FK_ProductServicePrices_ProductServices_ProductServiceID",
                        column: x => x.ProductServiceID,
                        principalTable: "ProductServices",
                        principalColumn: "ProductServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductServicePictures_ProductServiceID",
                table: "ProductServicePictures",
                column: "ProductServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServicePrices_ProductServiceID",
                table: "ProductServicePrices",
                column: "ProductServiceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductServicePictures");

            migrationBuilder.DropTable(
                name: "ProductServicePrices");

            migrationBuilder.DropTable(
                name: "ProductServices");
        }
    }
}
