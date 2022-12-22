using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class updateTypePKZ203 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MaxPayloadWeight",
                table: "MailLimits",
                type: "decimal(20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxPayloadVolume",
                table: "MailLimits",
                type: "decimal(20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxPayloadWeight",
                table: "CommPayloadsAt",
                type: "decimal(20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxPayloadVolume",
                table: "CommPayloadsAt",
                type: "decimal(20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxTakeOffWeight",
                table: "AircraftTypes",
                type: "decimal(20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxGrossPayload",
                table: "AircraftTypes",
                type: "decimal(20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxGrossCapacity",
                table: "AircraftTypes",
                type: "decimal(20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxTakeOffWeight",
                table: "Aircrafts",
                type: "decimal(20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxGrossPayload",
                table: "Aircrafts",
                type: "decimal(20,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MaxPayloadWeight",
                table: "MailLimits",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxPayloadVolume",
                table: "MailLimits",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxPayloadWeight",
                table: "CommPayloadsAt",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxPayloadVolume",
                table: "CommPayloadsAt",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxTakeOffWeight",
                table: "AircraftTypes",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxGrossPayload",
                table: "AircraftTypes",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxGrossCapacity",
                table: "AircraftTypes",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxTakeOffWeight",
                table: "Aircrafts",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxGrossPayload",
                table: "Aircrafts",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)");
        }
    }
}
