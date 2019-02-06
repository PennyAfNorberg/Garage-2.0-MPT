using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class splitttables_korr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Vehicles_VehicleId",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "ParkedVehicleId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ParkedVehicleId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Members");

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



            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_Vehicles_VehicleId",
                table: "ParkedVehicle",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Members_MemberId",
                table: "ParkedVehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Vehicles_VehicleId",
                table: "ParkedVehicle");

            migrationBuilder.AddColumn<int>(
                name: "ParkedVehicleId",
                table: "Vehicles",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "ParkedVehicleId",
                table: "Members",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Members",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_Members_MemberId",
                table: "ParkedVehicle",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_Vehicles_VehicleId",
                table: "ParkedVehicle",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
