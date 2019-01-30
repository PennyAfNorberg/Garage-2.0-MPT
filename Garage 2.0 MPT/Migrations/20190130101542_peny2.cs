using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class peny2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleTyp",
                columns: new[] { "VehicleTypId", "CostPerHour", "Name", "SpacesNeeded" },
                values: new object[,]
                {
                    { 1, 100, "Car", 1 },
                    { 2, 300, "Bus", 3 },
                    { 3, 50, "Motorbike", -3 },
                    { 4, 150, "Caravan", 1 },
                    { 5, 200, "RV", 2 },
                    { 6, 200, "Truck", 2 }
                });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId", "Where" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2019, 1, 29, 9, 12, 37, 740, DateTimeKind.Local).AddTicks(1721), null, "abc123", "Ferrari", "Green", "okänd", 1, null },
                    { 4, 4, new DateTime(2019, 1, 29, 8, 12, 37, 740, DateTimeKind.Local).AddTicks(2562), null, "abc456", "Ferrari", "Green", "okänd", 1, null },
                    { 7, 4, new DateTime(2019, 1, 29, 9, 12, 36, 740, DateTimeKind.Local).AddTicks(2578), null, "Rymdopera", "Ferrari", "Green", "okänd", 1, null },
                    { 2, 4, new DateTime(2019, 1, 28, 9, 12, 37, 740, DateTimeKind.Local).AddTicks(2482), null, "abc234", "Volvo", "Red", "okänd", 2, null },
                    { 5, 4, new DateTime(2019, 1, 28, 8, 12, 37, 740, DateTimeKind.Local).AddTicks(2568), null, "abc567", "Volvo", "Red", "okänd", 2, null },
                    { 8, 4, new DateTime(2019, 1, 28, 9, 12, 35, 740, DateTimeKind.Local).AddTicks(2585), null, "abc987", "Volvo", "Red", "okänd", 2, null },
                    { 3, 4, new DateTime(2019, 1, 26, 9, 12, 37, 740, DateTimeKind.Local).AddTicks(2495), new DateTime(2019, 1, 28, 8, 14, 56, 740, DateTimeKind.Local).AddTicks(2499), "abc345", "Saab", "Blue", "okänd", 5, null },
                    { 6, 4, new DateTime(2019, 1, 26, 9, 12, 37, 740, DateTimeKind.Local).AddTicks(2572), null, "abc678", "Saab", "Black", "okänd", 5, null },
                    { 9, 4, new DateTime(2019, 1, 26, 9, 12, 34, 740, DateTimeKind.Local).AddTicks(2591), null, "Biffen", "Bmv", "Black", "okänd", 5, null }
                });
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
    }
}
