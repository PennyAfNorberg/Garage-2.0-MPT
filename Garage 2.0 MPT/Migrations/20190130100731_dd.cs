using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class dd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ParkInDate", "RegNr" },
                values: new object[] { new DateTime(2019, 1, 29, 9, 4, 27, 95, DateTimeKind.Local).AddTicks(1734), "abc123" });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ParkInDate", "RegNr" },
                values: new object[] { new DateTime(2019, 1, 28, 9, 4, 27, 95, DateTimeKind.Local).AddTicks(2539), "abc234" });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate", "RegNr" },
                values: new object[] { new DateTime(2019, 1, 26, 9, 4, 27, 95, DateTimeKind.Local).AddTicks(2552), new DateTime(2019, 1, 28, 8, 6, 46, 95, DateTimeKind.Local).AddTicks(2555), "abc345" });

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId", "Where" },
                values: new object[,]
                {
                    { 4, 4, new DateTime(2019, 1, 29, 8, 4, 27, 95, DateTimeKind.Local).AddTicks(2608), null, "abc456", "Ferrari", "Green", "okänd", 1, null },
                    { 5, 4, new DateTime(2019, 1, 28, 8, 4, 27, 95, DateTimeKind.Local).AddTicks(2615), null, "abc567", "Volvo", "Red", "okänd", 2, null },
                    { 6, 4, new DateTime(2019, 1, 26, 9, 4, 27, 95, DateTimeKind.Local).AddTicks(2684), null, "abc678", "Saab", "Black", "okänd", 5, null },
                    { 7, 4, new DateTime(2019, 1, 29, 9, 4, 26, 95, DateTimeKind.Local).AddTicks(2691), null, "Rymdopera", "Ferrari", "Green", "okänd", 1, null },
                    { 8, 4, new DateTime(2019, 1, 28, 9, 4, 25, 95, DateTimeKind.Local).AddTicks(2698), null, "abc987", "Volvo", "Red", "okänd", 2, null },
                    { 9, 4, new DateTime(2019, 1, 26, 9, 4, 24, 95, DateTimeKind.Local).AddTicks(2704), null, "Biffen", "Bmv", "Black", "okänd", 5, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ParkInDate", "RegNr" },
                values: new object[] { new DateTime(2019, 1, 27, 8, 34, 49, 841, DateTimeKind.Local).AddTicks(280), "Rymdopera" });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ParkInDate", "RegNr" },
                values: new object[] { new DateTime(2019, 1, 26, 8, 34, 49, 841, DateTimeKind.Local).AddTicks(1181), "abc 123" });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate", "RegNr" },
                values: new object[] { new DateTime(2019, 1, 24, 8, 34, 49, 841, DateTimeKind.Local).AddTicks(1198), new DateTime(2019, 1, 26, 7, 37, 8, 841, DateTimeKind.Local).AddTicks(1201), "acc 123" });
        }
    }
}
