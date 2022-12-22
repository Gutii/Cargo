using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class MyFlightCorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Airline",
                table: "MyFlights");

            migrationBuilder.AddColumn<int>(
                name: "CarrierId",
                table: "MyFlights",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_MyFlights_CarrierId",
                table: "MyFlights",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyFlights_Carriers_CarrierId",
                table: "MyFlights",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFlights_Carriers_CarrierId",
                table: "MyFlights");

            migrationBuilder.DropIndex(
                name: "IX_MyFlights_CarrierId",
                table: "MyFlights");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "MyFlights");

            migrationBuilder.AddColumn<string>(
                name: "Airline",
                table: "MyFlights",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
