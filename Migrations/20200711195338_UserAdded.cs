using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.API.Migrations
{
    public partial class UserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleAccessories_Accessories_AccessoryId",
                table: "VehicleAccessories");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleAccessories_Vehicles_VehicleId",
                table: "VehicleAccessories");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleAccessories_Accessories_AccessoryId",
                table: "VehicleAccessories",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleAccessories_Vehicles_VehicleId",
                table: "VehicleAccessories",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleAccessories_Accessories_AccessoryId",
                table: "VehicleAccessories");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleAccessories_Vehicles_VehicleId",
                table: "VehicleAccessories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleAccessories_Accessories_AccessoryId",
                table: "VehicleAccessories",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleAccessories_Vehicles_VehicleId",
                table: "VehicleAccessories",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
