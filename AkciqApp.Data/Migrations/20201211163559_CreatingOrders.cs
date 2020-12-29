using Microsoft.EntityFrameworkCore.Migrations;

namespace AkciqApp.Data.Migrations
{
    public partial class CreatingOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalSum",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSum",
                table: "Orders");
        }
    }
}
