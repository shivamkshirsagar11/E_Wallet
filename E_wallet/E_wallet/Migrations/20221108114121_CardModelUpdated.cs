using Microsoft.EntityFrameworkCore.Migrations;

namespace E_wallet.Migrations
{
    public partial class CardModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ifsc",
                table: "Card",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ifsc",
                table: "Card");
        }
    }
}
