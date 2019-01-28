using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class AddWhereOnParkedVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SpacesNeeded",
                table: "VehicleTyp",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Where",
                table: "ParkedVehicle",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 27, 14, 8, 58, 466, DateTimeKind.Local).AddTicks(8241));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 26, 14, 8, 58, 466, DateTimeKind.Local).AddTicks(9154));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate" },
                values: new object[] { new DateTime(2019, 1, 24, 14, 8, 58, 466, DateTimeKind.Local).AddTicks(9168), new DateTime(2019, 1, 26, 13, 11, 17, 466, DateTimeKind.Local).AddTicks(9171) });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Where",
                table: "ParkedVehicle");

            migrationBuilder.AlterColumn<int>(
                name: "SpacesNeeded",
                table: "VehicleTyp",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
