using Microsoft.EntityFrameworkCore.Migrations;

namespace AkciqApp.Data.Migrations
{
    public partial class OrderIpAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmedPurchase",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedPurchase",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Orders");
        }
    }
}
