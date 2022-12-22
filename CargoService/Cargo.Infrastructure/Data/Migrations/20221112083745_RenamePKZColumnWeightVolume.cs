using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class RenamePKZColumnWeightVolume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxTakeOffWeight",
                table: "AircraftTypes",
                newName: "MaxPayloadWeight");

            migrationBuilder.RenameColumn(
                name: "MaxGrossPayload",
                table: "AircraftTypes",
                newName: "MaxPayloadVolume");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxPayloadWeight",
                table: "AircraftTypes",
                newName: "MaxTakeOffWeight");

            migrationBuilder.RenameColumn(
                name: "MaxPayloadVolume",
                table: "AircraftTypes",
                newName: "MaxGrossPayload");
        }
    }
}
