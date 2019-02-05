using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class mebers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 25, nullable: true),
                    LastName = table.Column<string>(maxLength: 25, nullable: true),
                    Address_Street = table.Column<string>(maxLength: 35, nullable: true),
                    Address_ZipCode = table.Column<string>(maxLength: 8, nullable: true),
                    Address_City = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(nullable: false),
                    PassWord = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkInDate",
                value: new DateTime(2019, 2, 4, 11, 13, 4, 937, DateTimeKind.Local).AddTicks(6571));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkInDate",
                value: new DateTime(2019, 2, 3, 11, 13, 4, 937, DateTimeKind.Local).AddTicks(7332));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate" },
                values: new object[] { new DateTime(2019, 2, 1, 11, 13, 4, 937, DateTimeKind.Local).AddTicks(7342), new DateTime(2019, 2, 3, 10, 15, 23, 937, DateTimeKind.Local).AddTicks(7346) });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParkInDate",
                value: new DateTime(2019, 2, 4, 10, 13, 4, 937, DateTimeKind.Local).AddTicks(7398));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "ParkInDate",
                value: new DateTime(2019, 2, 3, 10, 13, 4, 937, DateTimeKind.Local).AddTicks(7405));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 6,
                column: "ParkInDate",
                value: new DateTime(2019, 2, 1, 11, 13, 4, 937, DateTimeKind.Local).AddTicks(7412));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 7,
                column: "ParkInDate",
                value: new DateTime(2019, 2, 4, 11, 13, 3, 937, DateTimeKind.Local).AddTicks(7418));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 8,
                column: "ParkInDate",
                value: new DateTime(2019, 2, 3, 11, 13, 2, 937, DateTimeKind.Local).AddTicks(7425));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 9,
                column: "ParkInDate",
                value: new DateTime(2019, 2, 1, 11, 13, 1, 937, DateTimeKind.Local).AddTicks(7432));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 31, 15, 7, 43, 976, DateTimeKind.Local).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 30, 15, 7, 43, 976, DateTimeKind.Local).AddTicks(9551));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ParkInDate", "ParkOutDate" },
                values: new object[] { new DateTime(2019, 1, 28, 15, 7, 43, 976, DateTimeKind.Local).AddTicks(9557), new DateTime(2019, 1, 30, 14, 10, 2, 976, DateTimeKind.Local).AddTicks(9561) });

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 31, 14, 7, 43, 976, DateTimeKind.Local).AddTicks(9627));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 30, 14, 7, 43, 976, DateTimeKind.Local).AddTicks(9634));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 6,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 28, 15, 7, 43, 976, DateTimeKind.Local).AddTicks(9637));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 7,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 31, 15, 7, 42, 976, DateTimeKind.Local).AddTicks(9643));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 8,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 30, 15, 7, 41, 976, DateTimeKind.Local).AddTicks(9647));

            migrationBuilder.UpdateData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 9,
                column: "ParkInDate",
                value: new DateTime(2019, 1, 28, 15, 7, 40, 976, DateTimeKind.Local).AddTicks(9653));
        }
    }
}
