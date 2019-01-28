using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class MigrateToLinked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
