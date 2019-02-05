using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class splitttables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {



            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_VehicleTyp_VehicleTypId",
                table: "ParkedVehicle");

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 3);

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
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
    table: "ParkedVehicle",
    keyColumn: "Id",
    keyValue: 1008);

            migrationBuilder.DeleteData(
    table: "ParkedVehicle",
    keyColumn: "Id",
    keyValue: 1009);

    

            migrationBuilder.DropColumn(
                name: "RegNr",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "VehicleBrand",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "VehicleColor",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "VehicleModel",
                table: "ParkedVehicle");

            migrationBuilder.RenameColumn(
                name: "VehicleTypId",
                table: "ParkedVehicle",
                newName: "VehicleId");

            migrationBuilder.RenameColumn(
                name: "NumberOfWheels",
                table: "ParkedVehicle",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_ParkedVehicle_VehicleTypId",
                table: "ParkedVehicle",
                newName: "IX_ParkedVehicle_VehicleId");

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

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleTypId = table.Column<int>(nullable: false),
                    RegNr = table.Column<string>(nullable: false),
                    VehicleColor = table.Column<string>(nullable: true),
                    VehicleModel = table.Column<string>(nullable: true),
                    VehicleBrand = table.Column<string>(nullable: true),
                    NumberOfWheels = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: true),
                    ParkedVehicleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTyp_VehicleTypId",
                        column: x => x.VehicleTypId,
                        principalTable: "VehicleTyp",
                        principalColumn: "VehicleTypId",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "MemberId", "NumberOfWheels", "ParkedVehicleId", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId" },
                values: new object[,]
                {
                    { 1, null, 4, null, "abc123", "Ferrari", "Green", "okänd", 1 },
                    { 2, null, 4, null, "abc234", "Volvo", "Red", "okänd", 2 },
                    { 3, null, 4, null, "abc345", "Saab", "Blue", "okänd", 5 },
                    { 4, null, 4, null, "abc456", "Ferrari", "Green", "okänd", 1 },
                    { 5, null, 4, null, "abc567", "Volvo", "Red", "okänd", 2 },
                    { 6, null, 4, null, "abc678", "Saab", "Black", "okänd", 5 },
                    { 7, null, 4, null, "Rymdopera", "Ferrari", "Green", "okänd", 1 },
                    { 8, null, 4, null, "abc987", "Volvo", "Red", "okänd", 2 },
                    { 9, null, 4, null, "Biffen", "Bmv", "Black", "okänd", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_MemberId",
                table: "ParkedVehicle",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MemberId",
                table: "Vehicles",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypId",
                table: "Vehicles",
                column: "VehicleTypId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Members_MemberId",
                table: "ParkedVehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Vehicles_VehicleId",
                table: "ParkedVehicle");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_ParkedVehicle_MemberId",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "ParkedVehicleId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "ParkedVehicle",
                newName: "VehicleTypId");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "ParkedVehicle",
                newName: "NumberOfWheels");

            migrationBuilder.RenameIndex(
                name: "IX_ParkedVehicle_VehicleId",
                table: "ParkedVehicle",
                newName: "IX_ParkedVehicle_VehicleTypId");

            migrationBuilder.AddColumn<string>(
                name: "RegNr",
                table: "ParkedVehicle",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleBrand",
                table: "ParkedVehicle",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleColor",
                table: "ParkedVehicle",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleModel",
                table: "ParkedVehicle",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "Id", "NumberOfWheels", "ParkInDate", "ParkOutDate", "RegNr", "VehicleBrand", "VehicleColor", "VehicleModel", "VehicleTypId", "Where" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2019, 2, 4, 11, 13, 4, 937, DateTimeKind.Local).AddTicks(6571), null, "abc123", "Ferrari", "Green", "okänd", 1, null },
                    { 2, 4, new DateTime(2019, 2, 3, 11, 13, 4, 937, DateTimeKind.Local).AddTicks(7332), null, "abc234", "Volvo", "Red", "okänd", 2, null },
                    { 3, 4, new DateTime(2019, 2, 1, 11, 13, 4, 937, DateTimeKind.Local).AddTicks(7342), new DateTime(2019, 2, 3, 10, 15, 23, 937, DateTimeKind.Local).AddTicks(7346), "abc345", "Saab", "Blue", "okänd", 5, null },
                    { 4, 4, new DateTime(2019, 2, 4, 10, 13, 4, 937, DateTimeKind.Local).AddTicks(7398), null, "abc456", "Ferrari", "Green", "okänd", 1, null },
                    { 5, 4, new DateTime(2019, 2, 3, 10, 13, 4, 937, DateTimeKind.Local).AddTicks(7405), null, "abc567", "Volvo", "Red", "okänd", 2, null },
                    { 6, 4, new DateTime(2019, 2, 1, 11, 13, 4, 937, DateTimeKind.Local).AddTicks(7412), null, "abc678", "Saab", "Black", "okänd", 5, null },
                    { 7, 4, new DateTime(2019, 2, 4, 11, 13, 3, 937, DateTimeKind.Local).AddTicks(7418), null, "Rymdopera", "Ferrari", "Green", "okänd", 1, null },
                    { 8, 4, new DateTime(2019, 2, 3, 11, 13, 2, 937, DateTimeKind.Local).AddTicks(7425), null, "abc987", "Volvo", "Red", "okänd", 2, null },
                    { 9, 4, new DateTime(2019, 2, 1, 11, 13, 1, 937, DateTimeKind.Local).AddTicks(7432), null, "Biffen", "Bmv", "Black", "okänd", 5, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_VehicleTyp_VehicleTypId",
                table: "ParkedVehicle",
                column: "VehicleTypId",
                principalTable: "VehicleTyp",
                principalColumn: "VehicleTypId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
