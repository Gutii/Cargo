using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AwbWithTariffSln : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RateGridRankValue_TariffSolution_TariffSolutionId",
                table: "RateGridRankValue");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolution_IataLocations_AwbDestinationAirportId",
                table: "TariffSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolution_IataLocations_AwbOriginAirportId",
                table: "TariffSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolution_IataLocations_TransitAirportId",
                table: "TariffSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolution_RateGridHeaders_RateGridHeaderId",
                table: "TariffSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolution_TariffGroups_AwbDestinationTariffGroupId",
                table: "TariffSolution");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolution_TariffGroups_AwbOriginTariffGroupId",
                table: "TariffSolution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TariffSolution",
                table: "TariffSolution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RateGridRankValue",
                table: "RateGridRankValue");

            migrationBuilder.RenameTable(
                name: "TariffSolution",
                newName: "TariffSolutions");

            migrationBuilder.RenameTable(
                name: "RateGridRankValue",
                newName: "RateGridRankValues");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolution_TransitAirportId",
                table: "TariffSolutions",
                newName: "IX_TariffSolutions_TransitAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolution_RateGridHeaderId",
                table: "TariffSolutions",
                newName: "IX_TariffSolutions_RateGridHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolution_AwbOriginTariffGroupId",
                table: "TariffSolutions",
                newName: "IX_TariffSolutions_AwbOriginTariffGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolution_AwbOriginAirportId",
                table: "TariffSolutions",
                newName: "IX_TariffSolutions_AwbOriginAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolution_AwbDestinationTariffGroupId",
                table: "TariffSolutions",
                newName: "IX_TariffSolutions_AwbDestinationTariffGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolution_AwbDestinationAirportId",
                table: "TariffSolutions",
                newName: "IX_TariffSolutions_AwbDestinationAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_RateGridRankValue_TariffSolutionId",
                table: "RateGridRankValues",
                newName: "IX_RateGridRankValues_TariffSolutionId");

            migrationBuilder.AddColumn<bool>(
                name: "AllIn",
                table: "Awbs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<ulong>(
                name: "CollectId",
                table: "Awbs",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentProcedure",
                table: "Awbs",
                type: "varchar(16)",
                maxLength: 16,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<ulong>(
                name: "PrepaidId",
                table: "Awbs",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesChannel",
                table: "Awbs",
                type: "varchar(16)",
                maxLength: 16,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TariffClass",
                table: "Awbs",
                type: "varchar(16)",
                maxLength: 16,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TariffsSolutionCode",
                table: "Awbs",
                type: "varchar(16)",
                maxLength: 16,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Awbs",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TariffSolutions",
                table: "TariffSolutions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RateGridRankValues",
                table: "RateGridRankValues",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OtherCharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AwbId = table.Column<int>(type: "int", nullable: false),
                    TypeCharge = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CA = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prepaid = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Collect = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherCharges_Awbs_AwbId",
                        column: x => x.AwbId,
                        principalTable: "Awbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaxCharges",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Charge = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ValuationCharge = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalOtherChargesDueAgent = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalOtherChargesDueCarrier = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCharges", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Awbs_CollectId",
                table: "Awbs",
                column: "CollectId");

            migrationBuilder.CreateIndex(
                name: "IX_Awbs_PrepaidId",
                table: "Awbs",
                column: "PrepaidId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCharges_AwbId",
                table: "OtherCharges",
                column: "AwbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_TaxCharges_CollectId",
                table: "Awbs",
                column: "CollectId",
                principalTable: "TaxCharges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_TaxCharges_PrepaidId",
                table: "Awbs",
                column: "PrepaidId",
                principalTable: "TaxCharges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateGridRankValues_TariffSolutions_TariffSolutionId",
                table: "RateGridRankValues",
                column: "TariffSolutionId",
                principalTable: "TariffSolutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolutions_IataLocations_AwbDestinationAirportId",
                table: "TariffSolutions",
                column: "AwbDestinationAirportId",
                principalTable: "IataLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolutions_IataLocations_AwbOriginAirportId",
                table: "TariffSolutions",
                column: "AwbOriginAirportId",
                principalTable: "IataLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolutions_IataLocations_TransitAirportId",
                table: "TariffSolutions",
                column: "TransitAirportId",
                principalTable: "IataLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolutions_RateGridHeaders_RateGridHeaderId",
                table: "TariffSolutions",
                column: "RateGridHeaderId",
                principalTable: "RateGridHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolutions_TariffGroups_AwbDestinationTariffGroupId",
                table: "TariffSolutions",
                column: "AwbDestinationTariffGroupId",
                principalTable: "TariffGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolutions_TariffGroups_AwbOriginTariffGroupId",
                table: "TariffSolutions",
                column: "AwbOriginTariffGroupId",
                principalTable: "TariffGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_TaxCharges_CollectId",
                table: "Awbs");

            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_TaxCharges_PrepaidId",
                table: "Awbs");

            migrationBuilder.DropForeignKey(
                name: "FK_RateGridRankValues_TariffSolutions_TariffSolutionId",
                table: "RateGridRankValues");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolutions_IataLocations_AwbDestinationAirportId",
                table: "TariffSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolutions_IataLocations_AwbOriginAirportId",
                table: "TariffSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolutions_IataLocations_TransitAirportId",
                table: "TariffSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolutions_RateGridHeaders_RateGridHeaderId",
                table: "TariffSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolutions_TariffGroups_AwbDestinationTariffGroupId",
                table: "TariffSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_TariffSolutions_TariffGroups_AwbOriginTariffGroupId",
                table: "TariffSolutions");

            migrationBuilder.DropTable(
                name: "OtherCharges");

            migrationBuilder.DropTable(
                name: "TaxCharges");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_CollectId",
                table: "Awbs");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_PrepaidId",
                table: "Awbs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TariffSolutions",
                table: "TariffSolutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RateGridRankValues",
                table: "RateGridRankValues");

            migrationBuilder.DropColumn(
                name: "AllIn",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "CollectId",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "PaymentProcedure",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "PrepaidId",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "SalesChannel",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "TariffClass",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "TariffsSolutionCode",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Awbs");

            migrationBuilder.RenameTable(
                name: "TariffSolutions",
                newName: "TariffSolution");

            migrationBuilder.RenameTable(
                name: "RateGridRankValues",
                newName: "RateGridRankValue");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolutions_TransitAirportId",
                table: "TariffSolution",
                newName: "IX_TariffSolution_TransitAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolutions_RateGridHeaderId",
                table: "TariffSolution",
                newName: "IX_TariffSolution_RateGridHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolutions_AwbOriginTariffGroupId",
                table: "TariffSolution",
                newName: "IX_TariffSolution_AwbOriginTariffGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolutions_AwbOriginAirportId",
                table: "TariffSolution",
                newName: "IX_TariffSolution_AwbOriginAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolutions_AwbDestinationTariffGroupId",
                table: "TariffSolution",
                newName: "IX_TariffSolution_AwbDestinationTariffGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_TariffSolutions_AwbDestinationAirportId",
                table: "TariffSolution",
                newName: "IX_TariffSolution_AwbDestinationAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_RateGridRankValues_TariffSolutionId",
                table: "RateGridRankValue",
                newName: "IX_RateGridRankValue_TariffSolutionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TariffSolution",
                table: "TariffSolution",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RateGridRankValue",
                table: "RateGridRankValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RateGridRankValue_TariffSolution_TariffSolutionId",
                table: "RateGridRankValue",
                column: "TariffSolutionId",
                principalTable: "TariffSolution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolution_IataLocations_AwbDestinationAirportId",
                table: "TariffSolution",
                column: "AwbDestinationAirportId",
                principalTable: "IataLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolution_IataLocations_AwbOriginAirportId",
                table: "TariffSolution",
                column: "AwbOriginAirportId",
                principalTable: "IataLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolution_IataLocations_TransitAirportId",
                table: "TariffSolution",
                column: "TransitAirportId",
                principalTable: "IataLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolution_RateGridHeaders_RateGridHeaderId",
                table: "TariffSolution",
                column: "RateGridHeaderId",
                principalTable: "RateGridHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolution_TariffGroups_AwbDestinationTariffGroupId",
                table: "TariffSolution",
                column: "AwbDestinationTariffGroupId",
                principalTable: "TariffGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TariffSolution_TariffGroups_AwbOriginTariffGroupId",
                table: "TariffSolution",
                column: "AwbOriginTariffGroupId",
                principalTable: "TariffGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
