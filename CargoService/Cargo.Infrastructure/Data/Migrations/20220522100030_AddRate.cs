using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NlsLanguages",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false, comment: "ID and primary key of an NLS language.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, comment: "NLS language code. Must be a 3-char English mnemonic. Required. For language management.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true, comment: "NLS language name. Must be an English name. Optional. For language management.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ver = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false, comment: "Row's timestamp.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NlsLanguages", x => x.Id);
                },
                comment: "Contains all NLS languages supported by the system.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NlsResources",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false, comment: "ID and primary key of an NLS resource.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "Resource type such as string, image, huge text, etc..."),
                    Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "Resource code/identifier for user-friendly referencing with certain kinds of admin consoles (resource management).")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desc = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "Optional resource description for user-friendly resource management.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ver = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false, comment: "Row's timestamp.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NlsResources", x => x.Id);
                },
                comment: "Contains all NLS resources of all types supported by the system.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RateGridHeaders",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false, comment: "ID and primary key of a rate grid.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, comment: "Rate grid's code/name. Must be an English(10) unique string.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ver = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false, comment: "Row's timestamp.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateGridHeaders", x => x.Id);
                },
                comment: "Contains all read grid headers.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TariffGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionRus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionEng = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffGroups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NlsStringResources",
                columns: table => new
                {
                    LanId = table.Column<ulong>(type: "bigint unsigned", nullable: false, comment: "Reference to NLS language."),
                    RscId = table.Column<ulong>(type: "bigint unsigned", nullable: false, comment: "Reference to NLS resource."),
                    Value = table.Column<string>(type: "text", nullable: true, comment: "Actual NLS string/text.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ver = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false, comment: "Row's timestamp.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NlsStringResources", x => new { x.LanId, x.RscId });
                    table.ForeignKey(
                        name: "FK_NlsStringResources1",
                        column: x => x.LanId,
                        principalTable: "NlsLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NlsStringResources2",
                        column: x => x.RscId,
                        principalTable: "NlsResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Contains all string-type NLS resources supported by the system.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransProducts",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false, comment: "ID and primary key of a product.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "Product's code. Must be an English string.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescId = table.Column<ulong>(type: "bigint unsigned", nullable: false, comment: "Reference to description resource."),
                    Trigger = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, comment: "Product's trigger.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ver = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false, comment: "Row's timestamp.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransProducts1",
                        column: x => x.DescId,
                        principalTable: "NlsResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Contains all transportation products supported by the system.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RateGridRanks",
                columns: table => new
                {
                    GrdId = table.Column<ulong>(type: "bigint unsigned", nullable: false, comment: "Reference to grid's header."),
                    Rank = table.Column<uint>(type: "int unsigned", nullable: false, comment: "Grid's rank. Must be greater than or equal to zero."),
                    Ver = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false, comment: "Row's timestamp.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateGridRanks", x => new { x.GrdId, x.Rank });
                    table.ForeignKey(
                        name: "FK_RateGridRanks1",
                        column: x => x.GrdId,
                        principalTable: "RateGridHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Contains rank specifications of all rate grids.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TariffGroupAirport",
                columns: table => new
                {
                    AirportsId = table.Column<int>(type: "int", nullable: false),
                    TariffGroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffGroupAirport", x => new { x.AirportsId, x.TariffGroupsId });
                    table.ForeignKey(
                        name: "FK_TariffGroupAirport_IataLocations_AirportsId",
                        column: x => x.AirportsId,
                        principalTable: "IataLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TariffGroupAirport_TariffGroups_TariffGroupsId",
                        column: x => x.TariffGroupsId,
                        principalTable: "TariffGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShrTransProduct",
                columns: table => new
                {
                    ProductsId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    ShCodesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShrTransProduct", x => new { x.ProductsId, x.ShCodesId });
                    table.ForeignKey(
                        name: "FK_ShrTransProduct_Shrs_ShCodesId",
                        column: x => x.ShCodesId,
                        principalTable: "Shrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShrTransProduct_TransProducts_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "TransProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NlsLanguages1",
                table: "NlsLanguages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NlsResources1",
                table: "NlsResources",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NlsStringResources_RscId",
                table: "NlsStringResources",
                column: "RscId");

            migrationBuilder.CreateIndex(
                name: "IX_RateGridHeaders1",
                table: "RateGridHeaders",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShrTransProduct_ShCodesId",
                table: "ShrTransProduct",
                column: "ShCodesId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffGroupAirport_TariffGroupsId",
                table: "TariffGroupAirport",
                column: "TariffGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_TransProducts_DescId",
                table: "TransProducts",
                column: "DescId");

            migrationBuilder.CreateIndex(
                name: "IX_TransProducts1",
                table: "TransProducts",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransProducts2",
                table: "TransProducts",
                column: "Trigger",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NlsStringResources");

            migrationBuilder.DropTable(
                name: "RateGridRanks");

            migrationBuilder.DropTable(
                name: "ShrTransProduct");

            migrationBuilder.DropTable(
                name: "TariffGroupAirport");

            migrationBuilder.DropTable(
                name: "NlsLanguages");

            migrationBuilder.DropTable(
                name: "RateGridHeaders");

            migrationBuilder.DropTable(
                name: "TransProducts");

            migrationBuilder.DropTable(
                name: "TariffGroups");

            migrationBuilder.DropTable(
                name: "NlsResources");
        }
    }
}
