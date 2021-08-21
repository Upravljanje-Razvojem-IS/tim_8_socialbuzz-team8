using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthInfo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeOfIssuingPublicToken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublicToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrivateToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthInfo", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthInfo");
        }
    }
}
