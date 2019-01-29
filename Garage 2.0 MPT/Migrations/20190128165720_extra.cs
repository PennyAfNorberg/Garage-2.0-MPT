using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class extra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 27, 15, 54, 15, 366, DateTimeKind.Local).AddTicks(5446));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 26, 15, 54, 15, 366, DateTimeKind.Local).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate" },
                values: new object[] { new DateTime(2019, 1, 24, 15, 54, 15, 366, DateTimeKind.Local).AddTicks(5879), new DateTime(2019, 1, 26, 14, 56, 34, 366, DateTimeKind.Local).AddTicks(5882) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 27, 8, 32, 24, 287, DateTimeKind.Local).AddTicks(3127));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 26, 8, 32, 24, 287, DateTimeKind.Local).AddTicks(3925));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate" },
                values: new object[] { new DateTime(2019, 1, 24, 8, 32, 24, 287, DateTimeKind.Local).AddTicks(3938), new DateTime(2019, 1, 26, 7, 34, 43, 287, DateTimeKind.Local).AddTicks(3938) });
        }
    }
}
