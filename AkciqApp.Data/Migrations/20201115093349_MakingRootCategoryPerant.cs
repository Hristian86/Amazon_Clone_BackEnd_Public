using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AkciqApp.Data.Migrations
{
    public partial class MakingRootCategoryPerant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryPerantId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryPerant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPerant", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryPerantId",
                table: "Categories",
                column: "CategoryPerantId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPerant_IsDeleted",
                table: "CategoryPerant",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryPerant_CategoryPerantId",
                table: "Categories",
                column: "CategoryPerantId",
                principalTable: "CategoryPerant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryPerant_CategoryPerantId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryPerant");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryPerantId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryPerantId",
                table: "Categories");
        }
    }
}
