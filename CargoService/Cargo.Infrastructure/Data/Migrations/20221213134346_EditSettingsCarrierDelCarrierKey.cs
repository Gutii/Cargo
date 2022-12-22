using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class EditSettingsCarrierDelCarrierKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrierSettings_Carriers_CarrierId",
                table: "CarrierSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrierSettings",
                table: "CarrierSettings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CarrierSettings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CarrierSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrierSettings",
                table: "CarrierSettings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierSettings_CustomerId",
                table: "CarrierSettings",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrierSettings_Carriers_CustomerId",
                table: "CarrierSettings",
                column: "CustomerId",
                principalTable: "Carriers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrierSettings_Carriers_CustomerId",
                table: "CarrierSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrierSettings",
                table: "CarrierSettings");

            migrationBuilder.DropIndex(
                name: "IX_CarrierSettings_CustomerId",
                table: "CarrierSettings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CarrierSettings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CarrierSettings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrierSettings",
                table: "CarrierSettings",
                columns: new[] { "CarrierId", "ParametersSettingsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarrierSettings_Carriers_CarrierId",
                table: "CarrierSettings",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
