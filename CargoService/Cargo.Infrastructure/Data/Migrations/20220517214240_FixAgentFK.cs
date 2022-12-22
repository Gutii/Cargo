using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class FixAgentFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentsContracts_Agents_Id",
                table: "AgentsContracts");

            migrationBuilder.CreateIndex(
                name: "IX_AgentsContracts_SaleAgentId",
                table: "AgentsContracts",
                column: "SaleAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsContracts_Agents_SaleAgentId",
                table: "AgentsContracts",
                column: "SaleAgentId",
                principalTable: "Agents",
                principalColumn: "ContragentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentsContracts_Agents_SaleAgentId",
                table: "AgentsContracts");

            migrationBuilder.DropIndex(
                name: "IX_AgentsContracts_SaleAgentId",
                table: "AgentsContracts");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsContracts_Agents_Id",
                table: "AgentsContracts",
                column: "Id",
                principalTable: "Agents",
                principalColumn: "ContragentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
