using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class addPollAwnNumUnique2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Awbs_PoolAwbNumId",
                table: "Awbs",
                column: "PoolAwbNumId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbNums_PoolAwbNumId",
                table: "Awbs",
                column: "PoolAwbNumId",
                principalTable: "AgentsContractPoolAwbNums",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbNums_PoolAwbNumId",
                table: "Awbs");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_PoolAwbNumId",
                table: "Awbs");
        }
    }
}
