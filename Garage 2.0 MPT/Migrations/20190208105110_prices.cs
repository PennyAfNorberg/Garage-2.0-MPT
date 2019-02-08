using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class prices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VehicleBrand",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 1,
                column: "CostPerHour",
                value: 25);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 2,
                column: "CostPerHour",
                value: 75);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 3,
                column: "CostPerHour",
                value: 15);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 4,
                column: "CostPerHour",
                value: 95);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 5,
                column: "CostPerHour",
                value: 140);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 6,
                column: "CostPerHour",
                value: 140);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VehicleBrand",
                table: "Vehicles",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 1,
                column: "CostPerHour",
                value: 100);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 2,
                column: "CostPerHour",
                value: 300);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 3,
                column: "CostPerHour",
                value: 50);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 4,
                column: "CostPerHour",
                value: 150);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 5,
                column: "CostPerHour",
                value: 200);

            migrationBuilder.UpdateData(
                table: "VehicleTyp",
                keyColumn: "VehicleTypId",
                keyValue: 6,
                column: "CostPerHour",
                value: 200);
        }
    }
}
