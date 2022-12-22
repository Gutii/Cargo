using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class PollAwbNumDelCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "AgentsContractPoolAwbNums");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AgentsContractPoolAwbNums");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "AgentsContractPoolAwbNums",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AgentsContractPoolAwbNums",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
