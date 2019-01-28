using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class AddSpacesNeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "SpacesNeeded",
                table: "VehicleTyp",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 1,
                column: "SpacesNeeded",
                value: 1);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 2,
                column: "SpacesNeeded",
                value: 3);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 3,
                column: "SpacesNeeded",
                value: -3);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 4,
                column: "SpacesNeeded",
                value: 1);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 5,
                column: "SpacesNeeded",
                value: 2);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 6,
                column: "SpacesNeeded",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 27, 12, 42, 31, 954, DateTimeKind.Local).AddTicks(8926));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 26, 12, 42, 31, 954, DateTimeKind.Local).AddTicks(9797));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate" },
                values: new object[] { new DateTime(2019, 1, 24, 12, 42, 31, 954, DateTimeKind.Local).AddTicks(9810), new DateTime(2019, 1, 26, 11, 44, 50, 954, DateTimeKind.Local).AddTicks(9810) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpacesNeeded",
                table: "VehicleTyp");

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
