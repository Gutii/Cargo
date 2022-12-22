using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddColForwardindAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForwardingAgent",
                table: "Bookings",
                type: "varchar(17)",
                maxLength: 17,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ForwardingAgentId",
                table: "Bookings",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForwardingAgent",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ForwardingAgentId",
                table: "Bookings");
        }
    }
}
