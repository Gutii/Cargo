using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class IDelForwAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingsRcs_Awbs_AwbId",
                table: "BookingsRcs");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingsRcs_FlightShedules_FlightScheduleId",
                table: "BookingsRcs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingsRcs",
                table: "BookingsRcs");

            migrationBuilder.DropColumn(
                name: "ForwardingAgent",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "ForwardingAgentId",
                table: "Awbs");

            migrationBuilder.RenameTable(
                name: "BookingsRcs",
                newName: "BookingsArchive");

            migrationBuilder.RenameIndex(
                name: "IX_BookingsRcs_FlightScheduleId",
                table: "BookingsArchive",
                newName: "IX_BookingsArchive_FlightScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingsRcs_AwbId",
                table: "BookingsArchive",
                newName: "IX_BookingsArchive_AwbId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingsArchive",
                table: "BookingsArchive",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingsArchive_Awbs_AwbId",
                table: "BookingsArchive",
                column: "AwbId",
                principalTable: "Awbs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingsArchive_FlightShedules_FlightScheduleId",
                table: "BookingsArchive",
                column: "FlightScheduleId",
                principalTable: "FlightShedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingsArchive_Awbs_AwbId",
                table: "BookingsArchive");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingsArchive_FlightShedules_FlightScheduleId",
                table: "BookingsArchive");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingsArchive",
                table: "BookingsArchive");

            migrationBuilder.RenameTable(
                name: "BookingsArchive",
                newName: "BookingsRcs");

            migrationBuilder.RenameIndex(
                name: "IX_BookingsArchive_FlightScheduleId",
                table: "BookingsRcs",
                newName: "IX_BookingsRcs_FlightScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingsArchive_AwbId",
                table: "BookingsRcs",
                newName: "IX_BookingsRcs_AwbId");

            migrationBuilder.AddColumn<string>(
                name: "ForwardingAgent",
                table: "Awbs",
                type: "varchar(17)",
                maxLength: 17,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ForwardingAgentId",
                table: "Awbs",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingsRcs",
                table: "BookingsRcs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingsRcs_Awbs_AwbId",
                table: "BookingsRcs",
                column: "AwbId",
                principalTable: "Awbs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingsRcs_FlightShedules_FlightScheduleId",
                table: "BookingsRcs",
                column: "FlightScheduleId",
                principalTable: "FlightShedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
