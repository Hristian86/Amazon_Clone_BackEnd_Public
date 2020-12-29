using Microsoft.EntityFrameworkCore.Migrations;

namespace AkciqApp.Data.Migrations
{
    public partial class AddedTypeForDelimetar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
