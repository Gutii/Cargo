using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddCarrierCharges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrierCharges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionEng = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionRus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicationType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Recepient = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAllIn = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SalesChannels = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierCharges", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarrierChargeProductExcluded",
                columns: table => new
                {
                    ExcludedProductsId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    ExcludingCarrierChargesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierChargeProductExcluded", x => new { x.ExcludedProductsId, x.ExcludingCarrierChargesId });
                    table.ForeignKey(
                        name: "FK_CarrierChargeProductExcluded_CarrierCharges_ExcludingCarrier~",
                        column: x => x.ExcludingCarrierChargesId,
                        principalTable: "CarrierCharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrierChargeProductExcluded_TransportProducts_ExcludedProdu~",
                        column: x => x.ExcludedProductsId,
                        principalTable: "TransportProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarrierChargeProductIncluded",
                columns: table => new
                {
                    IncludedProductsId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    IncludingCarrierChargesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierChargeProductIncluded", x => new { x.IncludedProductsId, x.IncludingCarrierChargesId });
                    table.ForeignKey(
                        name: "FK_CarrierChargeProductIncluded_CarrierCharges_IncludingCarrier~",
                        column: x => x.IncludingCarrierChargesId,
                        principalTable: "CarrierCharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrierChargeProductIncluded_TransportProducts_IncludedProdu~",
                        column: x => x.IncludedProductsId,
                        principalTable: "TransportProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarrierChargeShrExcluded",
                columns: table => new
                {
                    ExcludedShrCodesId = table.Column<int>(type: "int", nullable: false),
                    ExcludingCarrierChargesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierChargeShrExcluded", x => new { x.ExcludedShrCodesId, x.ExcludingCarrierChargesId });
                    table.ForeignKey(
                        name: "FK_CarrierChargeShrExcluded_CarrierCharges_ExcludingCarrierChar~",
                        column: x => x.ExcludingCarrierChargesId,
                        principalTable: "CarrierCharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrierChargeShrExcluded_Shrs_ExcludedShrCodesId",
                        column: x => x.ExcludedShrCodesId,
                        principalTable: "Shrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarrierChargeShrIncluded",
                columns: table => new
                {
                    IncludedShrCodesId = table.Column<int>(type: "int", nullable: false),
                    IncludingCarrierChargesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierChargeShrIncluded", x => new { x.IncludedShrCodesId, x.IncludingCarrierChargesId });
                    table.ForeignKey(
                        name: "FK_CarrierChargeShrIncluded_CarrierCharges_IncludingCarrierChar~",
                        column: x => x.IncludingCarrierChargesId,
                        principalTable: "CarrierCharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrierChargeShrIncluded_Shrs_IncludedShrCodesId",
                        column: x => x.IncludedShrCodesId,
                        principalTable: "Shrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierChargeProductExcluded_ExcludingCarrierChargesId",
                table: "CarrierChargeProductExcluded",
                column: "ExcludingCarrierChargesId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierChargeProductIncluded_IncludingCarrierChargesId",
                table: "CarrierChargeProductIncluded",
                column: "IncludingCarrierChargesId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierChargeShrExcluded_ExcludingCarrierChargesId",
                table: "CarrierChargeShrExcluded",
                column: "ExcludingCarrierChargesId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierChargeShrIncluded_IncludingCarrierChargesId",
                table: "CarrierChargeShrIncluded",
                column: "IncludingCarrierChargesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrierChargeProductExcluded");

            migrationBuilder.DropTable(
                name: "CarrierChargeProductIncluded");

            migrationBuilder.DropTable(
                name: "CarrierChargeShrExcluded");

            migrationBuilder.DropTable(
                name: "CarrierChargeShrIncluded");

            migrationBuilder.DropTable(
                name: "CarrierCharges");
        }
    }
}
