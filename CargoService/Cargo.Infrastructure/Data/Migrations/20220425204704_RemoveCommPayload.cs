using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class RemoveCommPayload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwbNumber");

            migrationBuilder.DropTable(
                name: "CommPayloadRules4AicraftType");

            migrationBuilder.DropTable(
                name: "CommPayloadRules4Carrier");

            migrationBuilder.DropTable(
                name: "CommPayloadRules4Flight");

            migrationBuilder.DropTable(
                name: "CommPayloadRules4Route");

            migrationBuilder.DropTable(
                name: "PayloadNodes");

            migrationBuilder.DropTable(
                name: "Pool");

            migrationBuilder.DropTable(
                name: "CommercialPayloads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommercialPayloads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Volume = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommercialPayloads", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CarrierId = table.Column<int>(type: "int", nullable: false),
                    DateClose = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateOpen = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PoolLenght = table.Column<int>(type: "int", nullable: false),
                    SaleAgentId = table.Column<int>(type: "int", nullable: false),
                    StartNumber = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pool", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadRules4AicraftType",
                columns: table => new
                {
                    CommPayloadRuleId = table.Column<int>(type: "int", nullable: false),
                    AircraftTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadRules4AicraftType", x => x.CommPayloadRuleId);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4AicraftType_AircraftTypes_AircraftTypeId",
                        column: x => x.AircraftTypeId,
                        principalTable: "AircraftTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4AicraftType_CommercialPayloads_CommPayloadR~",
                        column: x => x.CommPayloadRuleId,
                        principalTable: "CommercialPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadRules4Carrier",
                columns: table => new
                {
                    CommPayloadRuleId = table.Column<int>(type: "int", nullable: false),
                    CarrierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadRules4Carrier", x => x.CommPayloadRuleId);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Carrier_Airlines_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Airlines",
                        principalColumn: "ContragentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Carrier_CommercialPayloads_CommPayloadRuleId",
                        column: x => x.CommPayloadRuleId,
                        principalTable: "CommercialPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadRules4Flight",
                columns: table => new
                {
                    CommPayloadRuleId = table.Column<int>(type: "int", nullable: false),
                    FlightScheduleId = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadRules4Flight", x => x.CommPayloadRuleId);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Flight_CommercialPayloads_CommPayloadRuleId",
                        column: x => x.CommPayloadRuleId,
                        principalTable: "CommercialPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Flight_FlightShedules_FlightScheduleId",
                        column: x => x.FlightScheduleId,
                        principalTable: "FlightShedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommPayloadRules4Route",
                columns: table => new
                {
                    CommPayloadRuleId = table.Column<int>(type: "int", nullable: false),
                    DestinationLocationId = table.Column<int>(type: "int", nullable: false),
                    OriginLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommPayloadRules4Route", x => x.CommPayloadRuleId);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Route_CommercialPayloads_CommPayloadRuleId",
                        column: x => x.CommPayloadRuleId,
                        principalTable: "CommercialPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Route_IataLocations_DestinationLocationId",
                        column: x => x.DestinationLocationId,
                        principalTable: "IataLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommPayloadRules4Route_IataLocations_OriginLocationId",
                        column: x => x.OriginLocationId,
                        principalTable: "IataLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PayloadNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActionToParent = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommPayloadId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayloadNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayloadNodes_CommercialPayloads_CommPayloadId",
                        column: x => x.CommPayloadId,
                        principalTable: "CommercialPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayloadNodes_PayloadNodes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "PayloadNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AwbNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PoolId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<int>(type: "int", maxLength: 7, nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwbNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwbNumber_Pool_PoolId",
                        column: x => x.PoolId,
                        principalTable: "Pool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AwbNumber_PoolId",
                table: "AwbNumber",
                column: "PoolId");

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadRules4AicraftType_AircraftTypeId",
                table: "CommPayloadRules4AicraftType",
                column: "AircraftTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadRules4Carrier_CarrierId",
                table: "CommPayloadRules4Carrier",
                column: "CarrierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadRules4Flight_FlightScheduleId",
                table: "CommPayloadRules4Flight",
                column: "FlightScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadRules4Route_DestinationLocationId",
                table: "CommPayloadRules4Route",
                column: "DestinationLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadRules4Route_OriginLocationId",
                table: "CommPayloadRules4Route",
                column: "OriginLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayloadNodes_CommPayloadId",
                table: "PayloadNodes",
                column: "CommPayloadId");

            migrationBuilder.CreateIndex(
                name: "IX_PayloadNodes_ParentId",
                table: "PayloadNodes",
                column: "ParentId");
        }
    }
}
