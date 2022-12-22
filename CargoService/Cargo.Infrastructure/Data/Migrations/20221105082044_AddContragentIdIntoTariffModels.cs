using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddContragentIdIntoTariffModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContragentId",
                table: "TransportProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContragentId",
                table: "TariffSolutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContragentId",
                table: "TariffGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContragentId",
                table: "RateGridHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContragentId",
                table: "CarrierCharges",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContragentId",
                table: "TransportProducts");

            migrationBuilder.DropColumn(
                name: "ContragentId",
                table: "TariffSolutions");

            migrationBuilder.DropColumn(
                name: "ContragentId",
                table: "TariffGroups");

            migrationBuilder.DropColumn(
                name: "ContragentId",
                table: "RateGridHeaders");

            migrationBuilder.DropColumn(
                name: "ContragentId",
                table: "CarrierCharges");
        }
    }
}
