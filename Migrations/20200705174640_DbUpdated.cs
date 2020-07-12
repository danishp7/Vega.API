using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.API.Migrations
{
    public partial class DbUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsCarCover = table.Column<bool>(nullable: false),
                    IsSeatCover = table.Column<bool>(nullable: false),
                    IsAirBags = table.Column<bool>(nullable: false),
                    IsSteeringLock = table.Column<bool>(nullable: false),
                    IsSteeringCover = table.Column<bool>(nullable: false),
                    IsPunctureKit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleAccessories",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false),
                    AccessoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleAccessories", x => new { x.VehicleId, x.AccessoryId });
                    table.ForeignKey(
                        name: "FK_VehicleAccessories_Accessories_AccessoryId",
                        column: x => x.AccessoryId,
                        principalTable: "Accessories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleAccessories_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAccessories_AccessoryId",
                table: "VehicleAccessories",
                column: "AccessoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleAccessories");

            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Vehicles");
        }
    }
}
