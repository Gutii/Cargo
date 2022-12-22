using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class ContragAgtOne2Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                 name: "FK_AgentsContracts_Agents_SaleAgentId",
                 table: "AgentsContracts");

             migrationBuilder.DropForeignKey(
                 name: "FK_Awbs_Contragents_AgentId",
                 table: "Awbs");

             migrationBuilder.DropForeignKey(
                 name: "FK_ContactInformations_Agents_AgentId",
                 table: "ContactInformations");

             migrationBuilder.DropPrimaryKey(
                 name: "PK_Agents",
                 table: "Agents");

             migrationBuilder.AddColumn<int>(
                 name: "Id",
                 table: "Agents",
                 type: "int",
                 nullable: false,
                 defaultValue: 0)
                 .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

             migrationBuilder.AddColumn<string>(
                 name: "Carrier",
                 table: "Agents",
                 type: "varchar(2)",
                 maxLength: 2,
                 nullable: false,
                 defaultValue: "")
                 .Annotation("MySql:CharSet", "utf8mb4");

             migrationBuilder.AddPrimaryKey(
                 name: "PK_Agents",
                 table: "Agents",
                 column: "Id");

             migrationBuilder.CreateIndex(
                 name: "IX_Agents_ContragentId",
                 table: "Agents",
                 column: "ContragentId");

             migrationBuilder.AddForeignKey(
                 name: "FK_AgentsContracts_Agents_SaleAgentId",
                 table: "AgentsContracts",
                 column: "SaleAgentId",
                 principalTable: "Agents",
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

             migrationBuilder.AddForeignKey(
                 name: "FK_Awbs_Agents_AgentId",
                 table: "Awbs",
                 column: "AgentId",
                 principalTable: "Agents",
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

             migrationBuilder.AddForeignKey(
                 name: "FK_ContactInformations_Agents_AgentId",
                 table: "ContactInformations",
                 column: "AgentId",
                 principalTable: "Agents",
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);*/


            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Contragents_ContragentId",
                table: "Agents",
                column: "ContragentId",
                principalTable: "Contragents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentsContracts_Agents_SaleAgentId",
                table: "AgentsContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_Agents_AgentId",
                table: "Awbs");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformations_Agents_AgentId",
                table: "ContactInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Agents_ContragentId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "Agents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "ContragentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentsContracts_Agents_SaleAgentId",
                table: "AgentsContracts",
                column: "SaleAgentId",
                principalTable: "Agents",
                principalColumn: "ContragentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_Contragents_AgentId",
                table: "Awbs",
                column: "AgentId",
                principalTable: "Contragents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformations_Agents_AgentId",
                table: "ContactInformations",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "ContragentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
