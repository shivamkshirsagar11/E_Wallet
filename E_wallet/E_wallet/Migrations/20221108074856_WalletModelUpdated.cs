using Microsoft.EntityFrameworkCore.Migrations;

namespace E_wallet.Migrations
{
    public partial class WalletModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Wallet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "BankI",
                table: "Wallet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastOperated",
                table: "Wallet",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TakeANote",
                table: "Wallet",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserI",
                table: "Wallet",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "BankI",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "LastOperated",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "TakeANote",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "UserI",
                table: "Wallet");
        }
    }
}
