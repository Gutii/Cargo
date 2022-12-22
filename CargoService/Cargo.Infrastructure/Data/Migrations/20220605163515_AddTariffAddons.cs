using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddTariffAddons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TariffAddons",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WeightAddon = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MinimumAddon = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TariffSolutionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffAddons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TariffAddons_TariffSolutions_TariffSolutionId",
                        column: x => x.TariffSolutionId,
                        principalTable: "TariffSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShrTariffAddon",
                columns: table => new
                {
                    ShrCodesId = table.Column<int>(type: "int", nullable: false),
                    TariffAddonsId = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShrTariffAddon", x => new { x.ShrCodesId, x.TariffAddonsId });
                    table.ForeignKey(
                        name: "FK_ShrTariffAddon_Shrs_ShrCodesId",
                        column: x => x.ShrCodesId,
                        principalTable: "Shrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShrTariffAddon_TariffAddons_TariffAddonsId",
                        column: x => x.TariffAddonsId,
                        principalTable: "TariffAddons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ShrTariffAddon_TariffAddonsId",
                table: "ShrTariffAddon",
                column: "TariffAddonsId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffAddons_TariffSolutionId",
                table: "TariffAddons",
                column: "TariffSolutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShrTariffAddon");

            migrationBuilder.DropTable(
                name: "TariffAddons");
        }
    }
}
