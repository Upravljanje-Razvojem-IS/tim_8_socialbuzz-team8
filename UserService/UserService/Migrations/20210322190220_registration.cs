using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class registration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAccountType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("067ea5db-9991-4ba3-80d1-821cc217fe3c"),
                column: "ConcurrencyStamp",
                value: "15fb1f10-fdd1-4c88-9584-2db8948b838c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8157308d-de73-435a-bda3-a91ad6d23c84"),
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "d37cf980-4d19-4fd3-834b-a120201d716b", "Role that basic level privileges" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3851f6d-8984-43b2-aecd-02b115687204"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TwoFactorEnabled", "UserAccountType" },
                values: new object[] { "a3f9a406-c93f-4ae2-9f73-b1f980b66d33", null, false, "Personal" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3851f6d-8984-43b2-aecd-02b125687004"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TwoFactorEnabled", "UserAccountType" },
                values: new object[] { "33f9cc6d-8303-4d39-910b-bd5c05689451", null, false, "Corporate" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3851f6d-8984-43b2-aecd-02b125687204"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TwoFactorEnabled", "UserAccountType" },
                values: new object[] { "6032d364-0020-4e9e-8955-c1bb307f9b50", null, false, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAccountType",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("067ea5db-9991-4ba3-80d1-821cc217fe3c"),
                column: "ConcurrencyStamp",
                value: "e104e5fd-86ef-4a15-8cfa-2da03218af02");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8157308d-de73-435a-bda3-a91ad6d23c84"),
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "67073a71-93ed-4a03-afe0-8162d1159c7a", "Role that enables root level privileges" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3851f6d-8984-43b2-aecd-02b115687204"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TwoFactorEnabled" },
                values: new object[] { "ec2b5ec5-57c6-446f-9dad-3a37f3f98f69", "l4g506m3", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3851f6d-8984-43b2-aecd-02b125687004"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TwoFactorEnabled" },
                values: new object[] { "2998b22e-bf7d-458b-989d-a1e8663d9e14", "l4dxvuqru0y12euh", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3851f6d-8984-43b2-aecd-02b125687204"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TwoFactorEnabled" },
                values: new object[] { "c373ae37-618e-41c0-a3a7-d363d73095fb", "x3x8tte0", true });
        }
    }
}
