using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class TestLinked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VehicleTyp",
                table: "ParkedVehicle",
                newName: "VehicleTypId");

            migrationBuilder.CreateTable(
                name: "VehicleTyp",
                columns: table => new
                {
                    VehicleTypId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CostPerHour = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTyp", x => x.VehicleTypId);
                });

            migrationBuilder.InsertData(
                table: "VehicleTyp",
                columns: new[] { "VehicleTypId", "CostPerHour", "Name" },
                values: new object[,]
                {
                    { 1, 100, "Car" },
                    { 2, 300, "Bus" },
                    { 3, 50, "Motorbike" },
                    { 4, 150, "Caravan" },
                    { 5, 200, "RV" },
                    { 6, 200, "Truck" }
                });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId" },
                values: new object[] { 1, 4, new DateTime(2019, 1, 24, 19, 4, 17, 432, DateTimeKind.Local).AddTicks(5442), null, "Rymdopera", "Ferrari", "Green", "okänd", 1 });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId" },
                values: new object[] { 2, 4, new DateTime(2019, 1, 23, 19, 4, 17, 432, DateTimeKind.Local).AddTicks(5905), null, "abc 123", "Volvo", "Red", "okänd", 2 });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId" },
                values: new object[] { 3, 4, new DateTime(2019, 1, 21, 19, 4, 17, 432, DateTimeKind.Local).AddTicks(5912), new DateTime(2019, 1, 23, 18, 6, 36, 432, DateTimeKind.Local).AddTicks(5914), "acc 123", "Saab", "Blue", "okänd", 5 });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_VehicleTypId",
                table: "ParkedVehicle",
                column: "VehicleTypId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_VehicleTyp_VehicleTypId",
                table: "ParkedVehicle",
                column: "VehicleTypId",
                principalTable: "VehicleTyp",
                principalColumn: "VehicleTypId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_VehicleTyp_VehicleTypId",
                table: "ParkedVehicle");

            migrationBuilder.DropTable(
                name: "VehicleTyp");

            migrationBuilder.DropIndex(
                name: "IX_ParkedVehicle_VehicleTypId",
                table: "ParkedVehicle");

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "VehicleTypId",
                table: "ParkedVehicle",
                newName: "VehicleTyp");
        }
    }
}
