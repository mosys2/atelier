using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Atelier.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "0af81db3-fc9c-4e56-8396-87a1782c7764");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "9d1fb279-7746-4fd1-bfc0-b4a3f48d2d9d");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "a96afce5-c2f1-43a6-8465-7077d1cad478");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "cf8d8b8f-10c1-4a89-b781-522198fe6f38");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "d964e41c-3654-4744-b13b-af2c144daaea");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Branches");

            //migrationBuilder.InsertData(
            //    table: "Roles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Description", "Discriminator", "InsertByUserId", "InsertTime", "IsRemoved", "Name", "NormalizedName", "PersianTitle", "RemoveByUserId", "RemoveTime", "UpdateByUserId", "UpdateTime" },
            //    values: new object[,]
            //    {
            //        { "0af81db3-fc9c-4e56-8396-87a1782c7764", null, null, "Role", null, new DateTime(2023, 12, 12, 13, 35, 46, 314, DateTimeKind.Local).AddTicks(9594), false, "Employee", "Employee", "منشی", null, null, null, null },
            //        { "9d1fb279-7746-4fd1-bfc0-b4a3f48d2d9d", null, null, "Role", null, new DateTime(2023, 12, 12, 13, 35, 46, 314, DateTimeKind.Local).AddTicks(9623), false, "Customer", "Customer", "منشی", null, null, null, null },
            //        { "a96afce5-c2f1-43a6-8465-7077d1cad478", null, null, "Role", null, new DateTime(2023, 12, 12, 13, 35, 46, 314, DateTimeKind.Local).AddTicks(9578), false, "Secretary", "Secretary", "منشی", null, null, null, null },
            //        { "cf8d8b8f-10c1-4a89-b781-522198fe6f38", null, null, "Role", null, new DateTime(2023, 12, 12, 13, 35, 46, 314, DateTimeKind.Local).AddTicks(9501), false, "BigAdmin", "BigAdmin", "مدیر اپلیکیشن", null, null, null, null },
            //        { "d964e41c-3654-4744-b13b-af2c144daaea", null, null, "Role", null, new DateTime(2023, 12, 12, 13, 35, 46, 314, DateTimeKind.Local).AddTicks(9570), false, "Admin", "Admin", "مدیر", null, null, null, null }
            //    });
        }
    }
}
