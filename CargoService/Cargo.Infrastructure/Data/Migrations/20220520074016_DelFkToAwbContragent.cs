using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class DelFkToAwbContragent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwbContragents_Awbs_AwbId",
                table: "AwbContragents");

            migrationBuilder.DropIndex(
                name: "IX_AwbContragents_AwbId",
                table: "AwbContragents");

            migrationBuilder.DropColumn(
                name: "AwbId",
                table: "AwbContragents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwbId",
                table: "AwbContragents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AwbContragents_AwbId",
                table: "AwbContragents",
                column: "AwbId");

            migrationBuilder.AddForeignKey(
                name: "FK_AwbContragents_Awbs_AwbId",
                table: "AwbContragents",
                column: "AwbId",
                principalTable: "Awbs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
