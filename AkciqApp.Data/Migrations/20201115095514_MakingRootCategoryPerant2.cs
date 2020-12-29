using Microsoft.EntityFrameworkCore.Migrations;

namespace AkciqApp.Data.Migrations
{
    public partial class MakingRootCategoryPerant2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryPerant_CategoryPerantId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryPerant",
                table: "CategoryPerant");

            migrationBuilder.RenameTable(
                name: "CategoryPerant",
                newName: "CategoryPerants");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryPerant_IsDeleted",
                table: "CategoryPerants",
                newName: "IX_CategoryPerants_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryPerants",
                table: "CategoryPerants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryPerants_CategoryPerantId",
                table: "Categories",
                column: "CategoryPerantId",
                principalTable: "CategoryPerants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryPerants_CategoryPerantId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryPerants",
                table: "CategoryPerants");

            migrationBuilder.RenameTable(
                name: "CategoryPerants",
                newName: "CategoryPerant");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryPerants_IsDeleted",
                table: "CategoryPerant",
                newName: "IX_CategoryPerant_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryPerant",
                table: "CategoryPerant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryPerant_CategoryPerantId",
                table: "Categories",
                column: "CategoryPerantId",
                principalTable: "CategoryPerant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
