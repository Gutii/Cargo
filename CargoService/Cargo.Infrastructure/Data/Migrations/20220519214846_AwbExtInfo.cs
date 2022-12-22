using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AwbExtInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_FlightShedules_FlightSheduleId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FlightSheduleId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FlightSheduleId",
                table: "Bookings");

            migrationBuilder.AddColumn<ulong>(
                name: "AgentId",
                table: "Awbs",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "ConsigneeId",
                table: "Awbs",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "ConsignorId",
                table: "Awbs",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManifestDescriptionOfGoodsRu",
                table: "Awbs",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfIssue",
                table: "Awbs",
                type: "varchar(60)",
                maxLength: 60,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<ulong>(
                name: "PoolAwbNumId",
                table: "Awbs",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialServiceRequestRu",
                table: "Awbs",
                type: "varchar(65)",
                maxLength: 65,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<ulong>(
                name: "Id",
                table: "AgentsContractPoolAwbNums",
                type: "bigint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateTable(
                name: "AwbContragents",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AwbId = table.Column<int>(type: "int", nullable: false),
                    NameRu = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameEn = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameExRu = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameExEn = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CityRu = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CityEn = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryISO = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ZipCode = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Passport = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegionRu = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegionEn = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeEn = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fax = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressRu = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressEn = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IataCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AgentCass = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwbContragents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwbContragents_Awbs_AwbId",
                        column: x => x.AwbId,
                        principalTable: "Awbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Awbs_AgentId",
                table: "Awbs",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Awbs_ConsigneeId",
                table: "Awbs",
                column: "ConsigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Awbs_ConsignorId",
                table: "Awbs",
                column: "ConsignorId");

            migrationBuilder.CreateIndex(
                name: "IX_Awbs_PoolAwbNumId",
                table: "Awbs",
                column: "PoolAwbNumId");

            migrationBuilder.CreateIndex(
                name: "IX_AwbContragents_AwbId",
                table: "AwbContragents",
                column: "AwbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbNums_PoolAwbNumId",
                table: "Awbs",
                column: "PoolAwbNumId",
                principalTable: "AgentsContractPoolAwbNums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_AwbContragents_AgentId",
                table: "Awbs",
                column: "AgentId",
                principalTable: "AwbContragents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_AwbContragents_ConsigneeId",
                table: "Awbs",
                column: "ConsigneeId",
                principalTable: "AwbContragents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_AwbContragents_ConsignorId",
                table: "Awbs",
                column: "ConsignorId",
                principalTable: "AwbContragents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbNums_PoolAwbNumId",
                table: "Awbs");

            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_AwbContragents_AgentId",
                table: "Awbs");

            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_AwbContragents_ConsigneeId",
                table: "Awbs");

            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_AwbContragents_ConsignorId",
                table: "Awbs");

            migrationBuilder.DropTable(
                name: "AwbContragents");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_AgentId",
                table: "Awbs");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_ConsigneeId",
                table: "Awbs");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_ConsignorId",
                table: "Awbs");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_PoolAwbNumId",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "ConsigneeId",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "ConsignorId",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "ManifestDescriptionOfGoodsRu",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "PlaceOfIssue",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "PoolAwbNumId",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "SpecialServiceRequestRu",
                table: "Awbs");

            migrationBuilder.AddColumn<ulong>(
                name: "FlightSheduleId",
                table: "Bookings",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AgentsContractPoolAwbNums",
                type: "int",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bigint unsigned")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightSheduleId",
                table: "Bookings",
                column: "FlightSheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_FlightShedules_FlightSheduleId",
                table: "Bookings",
                column: "FlightSheduleId",
                principalTable: "FlightShedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
