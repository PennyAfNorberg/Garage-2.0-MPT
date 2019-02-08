using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2._0_MPT.Migrations
{
    public partial class linkgarageuserswithmembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "GarageUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GarageUser_MemberId",
                table: "GarageUser",
                column: "MemberId");

  
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarageUser_Members_MemberId",
                table: "GarageUser");

            migrationBuilder.DropIndex(
                name: "IX_GarageUser_MemberId",
                table: "GarageUser");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "GarageUser");
        }
    }
}
