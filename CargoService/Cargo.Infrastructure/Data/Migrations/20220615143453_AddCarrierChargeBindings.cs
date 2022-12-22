using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddCarrierChargeBindings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrierChargeBindings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CarrierChargeId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    Parameter = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierChargeBindings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrierChargeBindings_CarrierCharges_CarrierChargeId",
                        column: x => x.CarrierChargeId,
                        principalTable: "CarrierCharges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarrierChargeBindings_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarrierChargeBindings_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarrierChargeBindingAirport",
                columns: table => new
                {
                    AirportsId = table.Column<int>(type: "int", nullable: false),
                    CarrierChargeBindingsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierChargeBindingAirport", x => new { x.AirportsId, x.CarrierChargeBindingsId });
                    table.ForeignKey(
                        name: "FK_CarrierChargeBindingAirport_CarrierChargeBindings_CarrierCha~",
                        column: x => x.CarrierChargeBindingsId,
                        principalTable: "CarrierChargeBindings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrierChargeBindingAirport_IataLocations_AirportsId",
                        column: x => x.AirportsId,
                        principalTable: "IataLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierChargeBindingAirport_CarrierChargeBindingsId",
                table: "CarrierChargeBindingAirport",
                column: "CarrierChargeBindingsId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierChargeBindings_CarrierChargeId",
                table: "CarrierChargeBindings",
                column: "CarrierChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierChargeBindings_CountryId",
                table: "CarrierChargeBindings",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierChargeBindings_CurrencyId",
                table: "CarrierChargeBindings",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrierChargeBindingAirport");

            migrationBuilder.DropTable(
                name: "CarrierChargeBindings");
        }
    }
}
