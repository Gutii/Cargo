using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddTariffSolutions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TariffSolution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValidationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoverageArea = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Currency = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsSpecial = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SalesChannel = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IataAgentCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Product = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PeriodType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AwbOriginAirportId = table.Column<int>(type: "int", nullable: true),
                    AwbDestinationAirportId = table.Column<int>(type: "int", nullable: true),
                    AwbOriginTariffGroupId = table.Column<int>(type: "int", nullable: true),
                    AwbDestinationTariffGroupId = table.Column<int>(type: "int", nullable: true),
                    TransitAirportId = table.Column<int>(type: "int", nullable: true),
                    Flights = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Routes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WeekDays = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentTerms = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WeightCharge = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAllIn = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TariffType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RateGridHeaderId = table.Column<ulong>(type: "bigint unsigned", nullable: true),
                    MinTariff = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffSolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TariffSolution_IataLocations_AwbDestinationAirportId",
                        column: x => x.AwbDestinationAirportId,
                        principalTable: "IataLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffSolution_IataLocations_AwbOriginAirportId",
                        column: x => x.AwbOriginAirportId,
                        principalTable: "IataLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffSolution_IataLocations_TransitAirportId",
                        column: x => x.TransitAirportId,
                        principalTable: "IataLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffSolution_RateGridHeaders_RateGridHeaderId",
                        column: x => x.RateGridHeaderId,
                        principalTable: "RateGridHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffSolution_TariffGroups_AwbDestinationTariffGroupId",
                        column: x => x.AwbDestinationTariffGroupId,
                        principalTable: "TariffGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TariffSolution_TariffGroups_AwbOriginTariffGroupId",
                        column: x => x.AwbOriginTariffGroupId,
                        principalTable: "TariffGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RateGridRankValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TariffSolutionId = table.Column<int>(type: "int", nullable: true),
                    Rank = table.Column<uint>(type: "int unsigned", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateGridRankValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RateGridRankValue_TariffSolution_TariffSolutionId",
                        column: x => x.TariffSolutionId,
                        principalTable: "TariffSolution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RateGridRankValue_TariffSolutionId",
                table: "RateGridRankValue",
                column: "TariffSolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffSolution_AwbDestinationAirportId",
                table: "TariffSolution",
                column: "AwbDestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffSolution_AwbDestinationTariffGroupId",
                table: "TariffSolution",
                column: "AwbDestinationTariffGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffSolution_AwbOriginAirportId",
                table: "TariffSolution",
                column: "AwbOriginAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffSolution_AwbOriginTariffGroupId",
                table: "TariffSolution",
                column: "AwbOriginTariffGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffSolution_RateGridHeaderId",
                table: "TariffSolution",
                column: "RateGridHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffSolution_TransitAirportId",
                table: "TariffSolution",
                column: "TransitAirportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RateGridRankValue");

            migrationBuilder.DropTable(
                name: "TariffSolution");
        }
    }
}
