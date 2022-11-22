using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Fix_MapBuilding_ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapFloors_MapFloors_MapBuildingId",
                table: "MapFloors");

            migrationBuilder.AddForeignKey(
                name: "FK_MapFloors_MapBuildings_MapBuildingId",
                table: "MapFloors",
                column: "MapBuildingId",
                principalTable: "MapBuildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapFloors_MapBuildings_MapBuildingId",
                table: "MapFloors");

            migrationBuilder.AddForeignKey(
                name: "FK_MapFloors_MapFloors_MapBuildingId",
                table: "MapFloors",
                column: "MapBuildingId",
                principalTable: "MapFloors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
