using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class BookingSerfRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routings");

            migrationBuilder.DropColumn(
                name: "ForwardingAgent",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Unloading",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ForwardingAgentId",
                table: "Bookings",
                newName: "PrevRoutingId");

            migrationBuilder.AddColumn<ulong>(
                name: "FlightScheduleId",
                table: "Bookings",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

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

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightScheduleId",
                table: "Bookings",
                column: "FlightScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PrevRoutingId",
                table: "Bookings",
                column: "PrevRoutingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Bookings_PrevRoutingId",
                table: "Bookings",
                column: "PrevRoutingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_FlightShedules_FlightScheduleId",
                table: "Bookings",
                column: "FlightScheduleId",
                principalTable: "FlightShedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Bookings_PrevRoutingId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_FlightShedules_FlightScheduleId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FlightScheduleId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PrevRoutingId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FlightScheduleId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ForwardingAgent",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "ForwardingAgentId",
                table: "Awbs");

            migrationBuilder.RenameColumn(
                name: "PrevRoutingId",
                table: "Bookings",
                newName: "ForwardingAgentId");

            migrationBuilder.AddColumn<string>(
                name: "ForwardingAgent",
                table: "Bookings",
                type: "varchar(17)",
                maxLength: 17,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Unloading",
                table: "Bookings",
                type: "varchar(3)",
                maxLength: 3,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Routings",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    FlightScheduleId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Seq = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routings_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Routings_FlightShedules_FlightScheduleId",
                        column: x => x.FlightScheduleId,
                        principalTable: "FlightShedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Routings_BookingId",
                table: "Routings",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Routings_FlightScheduleId",
                table: "Routings",
                column: "FlightScheduleId");
        }
    }
}
