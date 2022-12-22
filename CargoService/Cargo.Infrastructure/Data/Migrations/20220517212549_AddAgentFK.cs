using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddAgentFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleAgent",
                table: "AgentsContracts");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsContracts_Agents_Id",
                table: "AgentsContracts",
                column: "Id",
                principalTable: "Agents",
                principalColumn: "ContragentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentsContracts_Agents_Id",
                table: "AgentsContracts");

            migrationBuilder.AddColumn<string>(
                name: "SaleAgent",
                table: "AgentsContracts",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
