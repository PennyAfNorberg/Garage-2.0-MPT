using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class inittestdemo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 30, 8, 43, 7, 582, DateTimeKind.Local).AddTicks(8916));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 29, 8, 43, 7, 582, DateTimeKind.Local).AddTicks(9787));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate" },
                values: new object[] { new DateTime(2019, 1, 27, 8, 43, 7, 582, DateTimeKind.Local).AddTicks(9800), new DateTime(2019, 1, 29, 7, 45, 26, 582, DateTimeKind.Local).AddTicks(9803) });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 30, 7, 43, 7, 582, DateTimeKind.Local).AddTicks(9863));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 29, 7, 43, 7, 582, DateTimeKind.Local).AddTicks(9870));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 6,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 27, 8, 43, 7, 582, DateTimeKind.Local).AddTicks(9876));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 7,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 30, 8, 43, 6, 582, DateTimeKind.Local).AddTicks(9883));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 8,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 29, 8, 43, 5, 582, DateTimeKind.Local).AddTicks(9890));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 9,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 27, 8, 43, 4, 582, DateTimeKind.Local).AddTicks(9896));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 29, 9, 12, 37, 740, DateTimeKind.Local).AddTicks(1721));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 28, 9, 12, 37, 740, DateTimeKind.Local).AddTicks(2482));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate" },
                values: new object[] { new DateTime(2019, 1, 26, 9, 12, 37, 740, DateTimeKind.Local).AddTicks(2495), new DateTime(2019, 1, 28, 8, 14, 56, 740, DateTimeKind.Local).AddTicks(2499) });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 29, 8, 12, 37, 740, DateTimeKind.Local).AddTicks(2562));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 28, 8, 12, 37, 740, DateTimeKind.Local).AddTicks(2568));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 6,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 26, 9, 12, 37, 740, DateTimeKind.Local).AddTicks(2572));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 7,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 29, 9, 12, 36, 740, DateTimeKind.Local).AddTicks(2578));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 8,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 28, 9, 12, 35, 740, DateTimeKind.Local).AddTicks(2585));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 9,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 26, 9, 12, 34, 740, DateTimeKind.Local).AddTicks(2591));
        }
    }
}
