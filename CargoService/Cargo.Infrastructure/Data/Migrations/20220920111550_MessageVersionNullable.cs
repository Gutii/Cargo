using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargo.Infrastructure.Data.Migrations
{
    public partial class MessageVersionNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ushort>(
                name: "VersionNumber",
                table: "Messages",
                type: "smallint unsigned",
                nullable: true,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ushort>(
                name: "VersionNumber",
                table: "Messages",
                type: "smallint unsigned",
                nullable: false,
                defaultValue: (ushort)0,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned",
                oldNullable: true);
        }
    }
}
