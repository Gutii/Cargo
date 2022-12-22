using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddCommPayloadRule4FlightTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_CommPayloadRules4Flight_FlightScheduleId",
                table: "CommPayloadRules4Flight",
                column: "FlightScheduleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommPayloadRules4Flight");
        }
    }
}
