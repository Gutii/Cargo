using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class addPollAwnNumUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbNums_PoolAwbNumId",
                table: "Awbs");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_PoolAwbNumId",
                table: "Awbs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Awbs_PoolAwbNumId",
                table: "Awbs",
                column: "PoolAwbNumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbNums_PoolAwbNumId",
                table: "Awbs",
                column: "PoolAwbNumId",
                principalTable: "AgentsContractPoolAwbNums",
                principalColumn: "Id");
        }
    }
}
