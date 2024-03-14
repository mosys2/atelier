using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atelier.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialPageRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePagePermissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    RoleId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PageId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanAccess = table.Column<bool>(type: "bit", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePagePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePagePermissions_Pages_PageId1",
                        column: x => x.PageId1,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePagePermissions_Roles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePagePermissions_PageId1",
                table: "RolePagePermissions",
                column: "PageId1");

            migrationBuilder.CreateIndex(
                name: "IX_RolePagePermissions_RoleId1",
                table: "RolePagePermissions",
                column: "RoleId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePagePermissions");

            migrationBuilder.DropTable(
                name: "Pages");
        }
    }
}
