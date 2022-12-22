using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class ComposPkInAwbPollNums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbNums_PoolAwbNumId",
                table: "Awbs");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_PoolAwbNumId",
                table: "Awbs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgentsContractPoolAwbNums",
                table: "AgentsContractPoolAwbNums");

            migrationBuilder.DropIndex(
                name: "IX_AgentsContractPoolAwbNums_AwbPoolId",
                table: "AgentsContractPoolAwbNums");

            migrationBuilder.DropColumn(
                name: "PoolAwbNumId",
                table: "Awbs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AgentsContractPoolAwbNums");

            migrationBuilder.AddColumn<int>(
                name: "PoolAwbId",
                table: "Awbs",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgentsContractPoolAwbNums",
                table: "AgentsContractPoolAwbNums",
                columns: new[] { "AwbPoolId", "SerialNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Awbs_PoolAwbId",
                table: "Awbs",
                column: "PoolAwbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbs_PoolAwbId",
                table: "Awbs",
                column: "PoolAwbId",
                principalTable: "AgentsContractPoolAwbs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbs_PoolAwbId",
                table: "Awbs");

            migrationBuilder.DropIndex(
                name: "IX_Awbs_PoolAwbId",
                table: "Awbs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgentsContractPoolAwbNums",
                table: "AgentsContractPoolAwbNums");

            migrationBuilder.DropColumn(
                name: "PoolAwbId",
                table: "Awbs");

            migrationBuilder.AddColumn<ulong>(
                name: "PoolAwbNumId",
                table: "Awbs",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "Id",
                table: "AgentsContractPoolAwbNums",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgentsContractPoolAwbNums",
                table: "AgentsContractPoolAwbNums",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Awbs_PoolAwbNumId",
                table: "Awbs",
                column: "PoolAwbNumId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentsContractPoolAwbNums_AwbPoolId",
                table: "AgentsContractPoolAwbNums",
                column: "AwbPoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awbs_AgentsContractPoolAwbNums_PoolAwbNumId",
                table: "Awbs",
                column: "PoolAwbNumId",
                principalTable: "AgentsContractPoolAwbNums",
                principalColumn: "Id");
        }
    }
}
