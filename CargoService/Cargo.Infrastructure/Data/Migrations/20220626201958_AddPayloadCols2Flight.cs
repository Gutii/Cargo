using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddPayloadCols2Flight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PayloadVolume",
                table: "FlightShedules",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PayloadWeight",
                table: "FlightShedules",
                type: "double",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayloadVolume",
                table: "FlightShedules");

            migrationBuilder.DropColumn(
                name: "PayloadWeight",
                table: "FlightShedules");
        }
    }
}
