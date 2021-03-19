using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class InitialSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("067ea5db-9991-4ba3-80d1-821cc217fe3c"), "e104e5fd-86ef-4a15-8cfa-2da03218af02", "Admin", "Role that enables root level privileges", null },
                    { new Guid("8157308d-de73-435a-bda3-a91ad6d23c84"), "67073a71-93ed-4a03-afe0-8162d1159c7a", "Regular User", "Role that enables root level privileges", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountIsActive", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("b3851f6d-8984-43b2-aecd-02b115687204"), 0, true, "ec2b5ec5-57c6-446f-9dad-3a37f3f98f69", "fefa@gmail.com", false, false, null, null, null, "l4g506m3", "+38105050505", false, null, true, "fefolino" },
                    { new Guid("b3851f6d-8984-43b2-aecd-02b125687204"), 0, true, "c373ae37-618e-41c0-a3a7-d363d73095fb", "admin@gmail.com", false, false, null, null, null, "x3x8tte0", "+38105056665", false, null, true, "admin" },
                    { new Guid("b3851f6d-8984-43b2-aecd-02b125687004"), 0, true, "2998b22e-bf7d-458b-989d-a1e8663d9e14", "dexico@gmail.com", false, false, null, null, null, "l4dxvuqru0y12euh", "+01205050505", false, null, true, "Dexico" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("067ea5db-9991-4ba3-80d1-821cc217fe3c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8157308d-de73-435a-bda3-a91ad6d23c84"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3851f6d-8984-43b2-aecd-02b115687204"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3851f6d-8984-43b2-aecd-02b125687004"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3851f6d-8984-43b2-aecd-02b125687204"));
        }
    }
}
