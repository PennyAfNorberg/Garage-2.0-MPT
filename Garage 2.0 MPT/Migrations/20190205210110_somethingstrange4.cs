using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class somethingstrange4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Vehicles_VehicleId",
                table: "ParkedVehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Members_MemberId",
                table: "Vehicles");

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "ParkedVehicle",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "ParkedVehicle",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);




            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Members_MemberId",
                table: "Vehicles",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Members_MemberId",
                table: "ParkedVehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Vehicles_VehicleId",
                table: "ParkedVehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Members_MemberId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Vehicles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "ParkedVehicle",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "ParkedVehicle",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "MemberId", "NumberOfWheels", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId" },
                values: new object[,]
                {
                    { 1, null, 4, "abc123", "Ferrari", "Green", "okänd", 1 },
                    { 2, null, 4, "abc234", "Volvo", "Red", "okänd", 2 },
                    { 3, null, 4, "abc345", "Saab", "Blue", "okänd", 5 },
                    { 4, null, 4, "abc456", "Ferrari", "Green", "okänd", 1 },
                    { 5, null, 4, "abc567", "Volvo", "Red", "okänd", 2 },
                    { 6, null, 4, "abc678", "Saab", "Black", "okänd", 5 },
                    { 7, null, 4, "Rymdopera", "Ferrari", "Green", "okänd", 1 },
                    { 8, null, 4, "abc987", "Volvo", "Red", "okänd", 2 },
                    { 9, null, 4, "Biffen", "Bmv", "Black", "okänd", 5 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_Members_MemberId",
                table: "ParkedVehicle",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_Vehicles_VehicleId",
                table: "ParkedVehicle",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Members_MemberId",
                table: "Vehicles",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
