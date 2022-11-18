using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighLights",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Sells");

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "HighLights",
                table: "Sells",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "Sells",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
