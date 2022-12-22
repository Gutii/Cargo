using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class AddTitleToTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FlightDate",
                table: "ConsignmentStatuses",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "ConsignmentStatuses",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TitleRu",
                table: "ConsignmentStatuses",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "ConsignmentStatuses");

            migrationBuilder.DropColumn(
                name: "TitleRu",
                table: "ConsignmentStatuses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FlightDate",
                table: "ConsignmentStatuses",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
