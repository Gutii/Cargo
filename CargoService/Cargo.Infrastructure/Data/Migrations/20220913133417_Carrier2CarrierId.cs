using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class Carrier2CarrierId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "AgentsContractPoolAwbs");

            migrationBuilder.DropColumn(
                name: "DateClose",
                table: "AgentsContractPoolAwbs");

            migrationBuilder.DropColumn(
                name: "DateOpen",
                table: "AgentsContractPoolAwbs");

            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "Agents");

            migrationBuilder.AddColumn<int>(
                name: "CarrierId",
                table: "Agents",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Agents_CarrierId",
                table: "Agents",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Carriers_CarrierId",
                table: "Agents",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Carriers_CarrierId",
                table: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Agents_CarrierId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "Agents");

            migrationBuilder.AddColumn<string>(
                name: "Carrier",
                table: "AgentsContractPoolAwbs",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateClose",
                table: "AgentsContractPoolAwbs",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOpen",
                table: "AgentsContractPoolAwbs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Carrier",
                table: "Agents",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
