using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class DelTimurTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NlsStringResources");

            migrationBuilder.DropTable(
                name: "ShrTransProduct");

            migrationBuilder.DropTable(
                name: "NlsLanguages");

            migrationBuilder.DropTable(
                name: "TransProducts");

            migrationBuilder.DropTable(
                name: "NlsResources");

            migrationBuilder.DropColumn(
                name: "Ver",
                table: "RateGridRanks");

            migrationBuilder.DropColumn(
                name: "Ver",
                table: "RateGridHeaders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Ver",
                table: "RateGridRanks",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: false,
                comment: "Row's timestamp.")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ver",
                table: "RateGridHeaders",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: false,
                comment: "Row's timestamp.")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

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
                    Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "Resource code/identifier for user-friendly referencing with certain kinds of admin consoles (resource management).")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desc = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "Optional resource description for user-friendly resource management.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "Resource type such as string, image, huge text, etc..."),
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
                    DescId = table.Column<ulong>(type: "bigint unsigned", nullable: false, comment: "Reference to description resource."),
                    Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "Product's code. Must be an English string.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                name: "IX_ShrTransProduct_ShCodesId",
                table: "ShrTransProduct",
                column: "ShCodesId");

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
    }
}
