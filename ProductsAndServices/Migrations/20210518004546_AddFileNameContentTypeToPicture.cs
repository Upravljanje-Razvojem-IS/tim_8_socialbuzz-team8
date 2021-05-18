using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAndServices.Migrations
{
    public partial class AddFileNameContentTypeToPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ProductServicePictures",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ProductServicePictures",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ProductServicePictures");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ProductServicePictures");
        }
    }
}
