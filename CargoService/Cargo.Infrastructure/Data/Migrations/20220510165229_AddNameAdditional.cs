using Microsoft.EntityFrameworkCore.Migrations;

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddNameAdditional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameEngAdditional",
                table: "Contragents",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NameRusAdditional",
                table: "Contragents",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEngAdditional",
                table: "Contragents");

            migrationBuilder.DropColumn(
                name: "NameRusAdditional",
                table: "Contragents");
        }
    }
}
