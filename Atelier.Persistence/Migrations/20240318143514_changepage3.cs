using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atelier.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changepage3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageAccess_Roles_RoleId",
                table: "PageAccess");

            migrationBuilder.DropIndex(
                name: "IX_PageAccess_RoleId",
                table: "PageAccess");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "PageAccess");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "PageAccess",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageAccess_RoleId",
                table: "PageAccess",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageAccess_Roles_RoleId",
                table: "PageAccess",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
