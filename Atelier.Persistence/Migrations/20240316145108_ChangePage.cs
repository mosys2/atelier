using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atelier.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePagePermissions_Pages_PageId1",
                table: "RolePagePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePagePermissions_Roles_RoleId1",
                table: "RolePagePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePagePermissions_PageId1",
                table: "RolePagePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePagePermissions_RoleId1",
                table: "RolePagePermissions");

            migrationBuilder.DropColumn(
                name: "PageId1",
                table: "RolePagePermissions");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "RolePagePermissions");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "RolePagePermissions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "RolePagePermissions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RolePagePermissions_PageId",
                table: "RolePagePermissions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePagePermissions_RoleId",
                table: "RolePagePermissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePagePermissions_Pages_PageId",
                table: "RolePagePermissions",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePagePermissions_Roles_RoleId",
                table: "RolePagePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePagePermissions_Pages_PageId",
                table: "RolePagePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePagePermissions_Roles_RoleId",
                table: "RolePagePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePagePermissions_PageId",
                table: "RolePagePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePagePermissions_RoleId",
                table: "RolePagePermissions");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "RolePagePermissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "RolePagePermissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PageId1",
                table: "RolePagePermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "RolePagePermissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePagePermissions_PageId1",
                table: "RolePagePermissions",
                column: "PageId1");

            migrationBuilder.CreateIndex(
                name: "IX_RolePagePermissions_RoleId1",
                table: "RolePagePermissions",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePagePermissions_Pages_PageId1",
                table: "RolePagePermissions",
                column: "PageId1",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePagePermissions_Roles_RoleId1",
                table: "RolePagePermissions",
                column: "RoleId1",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
