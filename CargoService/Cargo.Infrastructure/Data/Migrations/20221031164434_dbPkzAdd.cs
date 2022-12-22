using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class dbPkzAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StDestinationLocal",
                table: "FlightShedules",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StOriginLocal",
                table: "FlightShedules",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Aircrafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OnboardNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxGrossPayload = table.Column<int>(type: "int", nullable: false),
                    MaxTakeOffWeight = table.Column<int>(type: "int", nullable: false),
                    AccseptedShr = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AircraftTypeId = table.Column<int>(type: "int", nullable: false),
                    IataCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aircrafts_AircraftTypes_AircraftTypeId",
                        column: x => x.AircraftTypeId,
                        principalTable: "AircraftTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadsAt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IataCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AircraftTypeId = table.Column<int>(type: "int", nullable: false),
                    AirlineId = table.Column<int>(type: "int", nullable: true),
                    FromIataLocationId = table.Column<int>(type: "int", nullable: true),
                    InIataLocationId = table.Column<int>(type: "int", nullable: true),
                    FlightNumber = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateStart = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MaxPayloadVolume = table.Column<int>(type: "int", nullable: false),
                    MaxPayloadWeight = table.Column<int>(type: "int", nullable: false),
                    AccseptedShr = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadsAt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommPayloadsAt_AircraftTypes_AircraftTypeId",
                        column: x => x.AircraftTypeId,
                        principalTable: "AircraftTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommPayloadsAt_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "ContragentId");
                    table.ForeignKey(
                        name: "FK_CommPayloadsAt_IataLocations_FromIataLocationId",
                        column: x => x.FromIataLocationId,
                        principalTable: "IataLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommPayloadsAt_IataLocations_InIataLocationId",
                        column: x => x.InIataLocationId,
                        principalTable: "IataLocations",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MailLimits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IataCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AircraftTypeId = table.Column<int>(type: "int", nullable: false),
                    AirlineId = table.Column<int>(type: "int", nullable: true),
                    FromIataLocationId = table.Column<int>(type: "int", nullable: true),
                    InIataLocationId = table.Column<int>(type: "int", nullable: true),
                    FlightNumber = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateStart = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MaxPayloadVolume = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MaxPayloadWeight = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailLimits_AircraftTypes_AircraftTypeId",
                        column: x => x.AircraftTypeId,
                        principalTable: "AircraftTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MailLimits_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "ContragentId");
                    table.ForeignKey(
                        name: "FK_MailLimits_IataLocations_FromIataLocationId",
                        column: x => x.FromIataLocationId,
                        principalTable: "IataLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MailLimits_IataLocations_InIataLocationId",
                        column: x => x.InIataLocationId,
                        principalTable: "IataLocations",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Aircrafts_AircraftTypeId",
                table: "Aircrafts",
                column: "AircraftTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadsAt_AircraftTypeId",
                table: "CommPayloadsAt",
                column: "AircraftTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadsAt_AirlineId",
                table: "CommPayloadsAt",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadsAt_FromIataLocationId",
                table: "CommPayloadsAt",
                column: "FromIataLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadsAt_InIataLocationId",
                table: "CommPayloadsAt",
                column: "InIataLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MailLimits_AircraftTypeId",
                table: "MailLimits",
                column: "AircraftTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MailLimits_AirlineId",
                table: "MailLimits",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_MailLimits_FromIataLocationId",
                table: "MailLimits",
                column: "FromIataLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MailLimits_InIataLocationId",
                table: "MailLimits",
                column: "InIataLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aircrafts");

            migrationBuilder.DropTable(
                name: "CommPayloadsAt");

            migrationBuilder.DropTable(
                name: "MailLimits");

            migrationBuilder.DropColumn(
                name: "StDestinationLocal",
                table: "FlightShedules");

            migrationBuilder.DropColumn(
                name: "StOriginLocal",
                table: "FlightShedules");
        }
    }
}
