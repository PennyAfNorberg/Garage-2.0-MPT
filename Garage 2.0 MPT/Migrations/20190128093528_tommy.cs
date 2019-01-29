using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class tommy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 27, 7, 57, 27, 241, DateTimeKind.Local).AddTicks(9452));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 26, 7, 57, 27, 242, DateTimeKind.Local).AddTicks(406));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate" },
                values: new object[] { new DateTime(2019, 1, 24, 7, 57, 27, 242, DateTimeKind.Local).AddTicks(415), new DateTime(2019, 1, 26, 6, 59, 46, 242, DateTimeKind.Local).AddTicks(419) });
        }
    }
}
