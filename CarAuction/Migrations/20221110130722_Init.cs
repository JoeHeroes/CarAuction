using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVehicle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bidStatus = table.Column<bool>(type: "bit", nullable: false),
                    currentBid = table.Column<int>(type: "int", nullable: false),
                    saleStatus = table.Column<bool>(type: "bit", nullable: false),
                    location = table.Column<int>(type: "int", nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    timeLeft = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfoSells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVehicle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    saleTerm = table.Column<int>(type: "int", nullable: false),
                    primaryDamage = table.Column<int>(type: "int", nullable: false),
                    secondaryDamage = table.Column<int>(type: "int", nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    highLights = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoSells", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVehicle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    producer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modelSpecifer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modelGeneration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    registrationYear = table.Column<int>(type: "int", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bodyType = table.Column<int>(type: "int", nullable: false),
                    engineCapacity = table.Column<int>(type: "int", nullable: false),
                    engineOutput = table.Column<int>(type: "int", nullable: false),
                    transmission = table.Column<int>(type: "int", nullable: false),
                    drive = table.Column<int>(type: "int", nullable: false),
                    meterReadout = table.Column<long>(type: "bigint", nullable: false),
                    fuel = table.Column<int>(type: "int", nullable: false),
                    numberKeys = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serviceManual = table.Column<bool>(type: "bit", nullable: false),
                    secondTireSet = table.Column<bool>(type: "bit", nullable: false),
                    CreateById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "InfoSells");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
