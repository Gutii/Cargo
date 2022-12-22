using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class DelCommPayload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommPayloadNodes");

            migrationBuilder.DropTable(
                name: "CommPayloadRules4AicraftType");

            migrationBuilder.DropTable(
                name: "CommPayloadRules4Carrier");

            migrationBuilder.DropTable(
                name: "CommPayloadRules4Flight");

            migrationBuilder.DropTable(
                name: "CommPayloadRules4Route");

            migrationBuilder.DropTable(
                name: "CommPayloads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessagesProperties");

            migrationBuilder.DropTable(
                name: "MessageIdentifiers");

            migrationBuilder.CreateTable(
                name: "CommPayloads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Volume = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloads", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CommPayloadId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ActionToParent = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommPayloadNodes_CommPayloadNodes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CommPayloadNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommPayloadNodes_CommPayloads_CommPayloadId",
                        column: x => x.CommPayloadId,
                        principalTable: "CommPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadRules4AicraftType",
                columns: table => new
                {
                    CommPayloadRuleId = table.Column<int>(type: "int", nullable: false),
                    AircraftType = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AircraftTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadRules4AicraftType", x => x.CommPayloadRuleId);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4AicraftType_CommPayloads_CommPayloadRuleId",
                        column: x => x.CommPayloadRuleId,
                        principalTable: "CommPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadRules4Carrier",
                columns: table => new
                {
                    CommPayloadRuleId = table.Column<int>(type: "int", nullable: false),
                    Carrier = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadRules4Carrier", x => x.CommPayloadRuleId);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Carrier_CommPayloads_CommPayloadRuleId",
                        column: x => x.CommPayloadRuleId,
                        principalTable: "CommPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadRules4Flight",
                columns: table => new
                {
                    CommPayloadRuleId = table.Column<int>(type: "int", nullable: false),
                    DateAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FlightCarrier = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlightNumber = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadRules4Flight", x => x.CommPayloadRuleId);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Flight_CommPayloads_CommPayloadRuleId",
                        column: x => x.CommPayloadRuleId,
                        principalTable: "CommPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadRules4Route",
                columns: table => new
                {
                    CommPayloadRuleId = table.Column<int>(type: "int", nullable: false),
                    Destination = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Origin = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadRules4Route", x => x.CommPayloadRuleId);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Route_CommPayloads_CommPayloadRuleId",
                        column: x => x.CommPayloadRuleId,
                        principalTable: "CommPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadNodes_CommPayloadId",
                table: "CommPayloadNodes",
                column: "CommPayloadId");

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadNodes_ParentId",
                table: "CommPayloadNodes",
                column: "ParentId");
        }
    }
}
