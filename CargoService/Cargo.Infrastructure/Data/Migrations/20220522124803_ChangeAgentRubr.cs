using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class ChangeAgentRubr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_AwbContragents_AgentId",
                table: "Awbs");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Awbs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(ulong),
                oldType: "bigint unsigned",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_Contragents_AgentId",
                table: "Awbs",
                column: "AgentId",
                principalTable: "Contragents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_Contragents_AgentId",
                table: "Awbs");

            migrationBuilder.AlterColumn<ulong>(
                name: "AgentId",
                table: "Awbs",
                type: "bigint unsigned",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_AwbContragents_AgentId",
                table: "Awbs",
                column: "AgentId",
                principalTable: "AwbContragents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
