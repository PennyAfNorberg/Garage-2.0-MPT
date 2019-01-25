using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkedVehicle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleTyp = table.Column<int>(nullable: false),
                    RegNr = table.Column<string>(nullable: true),
                    VehicleColor = table.Column<string>(nullable: true),
                    VehicleModel = table.Column<string>(nullable: true),
                    VehicleBrand = table.Column<string>(nullable: true),
                    NumberOfWheels = table.Column<int>(nullable: false),
                    ParkInDate = table.Column<DateTime>(nullable: false),
                    ParkOutDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkedVehicle", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkedVehicle");
        }
    }
}
