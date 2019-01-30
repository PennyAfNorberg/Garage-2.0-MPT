using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class peny : Migration
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

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleTyp",
                columns: new[] { "VehicleTypId", "CostPerHour", "Name", "SpacesNeeded" },
                values: new object[,]
                {
                    { 1, 100, "Car", 0 },
                    { 2, 300, "Bus", 0 },
                    { 3, 50, "Motorbike", 0 },
                    { 4, 150, "Caravan", 0 },
                    { 5, 200, "RV", 0 },
                    { 6, 200, "Truck", 0 }
                });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId", "Where" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2019, 1, 29, 9, 4, 27, 95, DateTimeKind.Local).AddTicks(1734), null, "abc123", "Ferrari", "Green", "okänd", 1, null },
                    { 4, 4, new DateTime(2019, 1, 29, 8, 4, 27, 95, DateTimeKind.Local).AddTicks(2608), null, "abc456", "Ferrari", "Green", "okänd", 1, null },
                    { 7, 4, new DateTime(2019, 1, 29, 9, 4, 26, 95, DateTimeKind.Local).AddTicks(2691), null, "Rymdopera", "Ferrari", "Green", "okänd", 1, null },
                    { 2, 4, new DateTime(2019, 1, 28, 9, 4, 27, 95, DateTimeKind.Local).AddTicks(2539), null, "abc234", "Volvo", "Red", "okänd", 2, null },
                    { 5, 4, new DateTime(2019, 1, 28, 8, 4, 27, 95, DateTimeKind.Local).AddTicks(2615), null, "abc567", "Volvo", "Red", "okänd", 2, null },
                    { 8, 4, new DateTime(2019, 1, 28, 9, 4, 25, 95, DateTimeKind.Local).AddTicks(2698), null, "abc987", "Volvo", "Red", "okänd", 2, null },
                    { 3, 4, new DateTime(2019, 1, 26, 9, 4, 27, 95, DateTimeKind.Local).AddTicks(2552), new DateTime(2019, 1, 28, 8, 6, 46, 95, DateTimeKind.Local).AddTicks(2555), "abc345", "Saab", "Blue", "okänd", 5, null },
                    { 6, 4, new DateTime(2019, 1, 26, 9, 4, 27, 95, DateTimeKind.Local).AddTicks(2684), null, "abc678", "Saab", "Black", "okänd", 5, null },
                    { 9, 4, new DateTime(2019, 1, 26, 9, 4, 24, 95, DateTimeKind.Local).AddTicks(2704), null, "Biffen", "Bmv", "Black", "okänd", 5, null }
                });
        }
    }
}
