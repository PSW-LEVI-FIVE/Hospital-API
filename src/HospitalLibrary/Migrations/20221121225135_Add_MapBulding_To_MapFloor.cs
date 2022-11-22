using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Add_MapBulding_To_MapFloor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MapBuildingId",
                table: "MapFloors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MapFloors_MapBuildingId",
                table: "MapFloors",
                column: "MapBuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapFloors_MapFloors_MapBuildingId",
                table: "MapFloors",
                column: "MapBuildingId",
                principalTable: "MapFloors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapFloors_MapFloors_MapBuildingId",
                table: "MapFloors");

            migrationBuilder.DropIndex(
                name: "IX_MapFloors_MapBuildingId",
                table: "MapFloors");

            migrationBuilder.DropColumn(
                name: "MapBuildingId",
                table: "MapFloors");
        }
    }
}
