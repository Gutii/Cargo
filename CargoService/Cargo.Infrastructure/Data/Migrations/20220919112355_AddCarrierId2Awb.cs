using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddCarrierId2Awb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_Agents_AgentId",
                table: "Awbs");

            migrationBuilder.AddColumn<int>(
                name: "CarrierId",
                table: "Awbs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Awbs_CarrierId",
                table: "Awbs",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_Carriers_CarrierId",
                table: "Awbs",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_Contragents_AgentId",
                table: "Awbs",
                column: "AgentId",
                principalTable: "Contragents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_Carriers_CarrierId",
                table: "Awbs");

            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_Contragents_AgentId",
                table: "Awbs");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_CarrierId",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "Awbs");

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_Agents_AgentId",
                table: "Awbs",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }
    }
}
