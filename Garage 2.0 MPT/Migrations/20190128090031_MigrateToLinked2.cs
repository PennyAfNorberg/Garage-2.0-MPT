using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class MigrateToLinked2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId" },
                values: new object[] { 1, 4, new DateTime(2019, 1, 27, 7, 57, 27, 241, DateTimeKind.Local).AddTicks(9452), null, "Rymdopera", "Ferrari", "Green", "okänd", 1 });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId" },
                values: new object[] { 2, 4, new DateTime(2019, 1, 26, 7, 57, 27, 242, DateTimeKind.Local).AddTicks(406), null, "abc 123", "Volvo", "Red", "okänd", 2 });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId" },
                values: new object[] { 3, 4, new DateTime(2019, 1, 24, 7, 57, 27, 242, DateTimeKind.Local).AddTicks(415), new DateTime(2019, 1, 26, 6, 59, 46, 242, DateTimeKind.Local).AddTicks(419), "acc 123", "Saab", "Blue", "okänd", 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
