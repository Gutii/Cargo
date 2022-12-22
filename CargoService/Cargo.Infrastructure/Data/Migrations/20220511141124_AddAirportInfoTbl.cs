using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddAirportInfoTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IataLocationExtInfos",
                columns: table => new
                {
                    IataLocationId = table.Column<int>(type: "int", nullable: false),
                    CityRusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeZoneSummer = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    TimeZoneWinter = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Remarks = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IataLocationExtInfos", x => x.IataLocationId);
                    table.ForeignKey(
                        name: "FK_IataLocationExtInfos_IataLocations_IataLocationId",
                        column: x => x.IataLocationId,
                        principalTable: "IataLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AirportContactInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Position = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone1 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone2 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email1 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email2 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IataLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportContactInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirportContactInformations_IataLocationExtInfos_IataLocation~",
                        column: x => x.IataLocationId,
                        principalTable: "IataLocationExtInfos",
                        principalColumn: "IataLocationId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SlaProhibitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Shr = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MvlVvl = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Import = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Export = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Transfer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Transit = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IataLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlaProhibitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlaProhibitions_IataLocationExtInfos_IataLocationId",
                        column: x => x.IataLocationId,
                        principalTable: "IataLocationExtInfos",
                        principalColumn: "IataLocationId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SlaTimeLimitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Shr = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MvlVvl = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Time = table.Column<int>(type: "int", nullable: false),
                    IataLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlaTimeLimitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlaTimeLimitations_IataLocationExtInfos_IataLocationId",
                        column: x => x.IataLocationId,
                        principalTable: "IataLocationExtInfos",
                        principalColumn: "IataLocationId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TelexSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OffsetBase = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OffsetValue = table.Column<int>(type: "int", nullable: true),
                    Emails = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IataLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelexSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelexSettings_IataLocationExtInfos_IataLocationId",
                        column: x => x.IataLocationId,
                        principalTable: "IataLocationExtInfos",
                        principalColumn: "IataLocationId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AirportContactInformations_IataLocationId",
                table: "AirportContactInformations",
                column: "IataLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SlaProhibitions_IataLocationId",
                table: "SlaProhibitions",
                column: "IataLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SlaTimeLimitations_IataLocationId",
                table: "SlaTimeLimitations",
                column: "IataLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TelexSettings_IataLocationId",
                table: "TelexSettings",
                column: "IataLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirportContactInformations");

            migrationBuilder.DropTable(
                name: "SlaProhibitions");

            migrationBuilder.DropTable(
                name: "SlaTimeLimitations");

            migrationBuilder.DropTable(
                name: "TelexSettings");

            migrationBuilder.DropTable(
                name: "IataLocationExtInfos");
        }
    }
}
