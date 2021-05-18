using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAndServices.Migrations
{
    public partial class AddCreatedByUserIDToProductService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserID",
                table: "ProductServices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "ProductServices");
        }
    }
}
