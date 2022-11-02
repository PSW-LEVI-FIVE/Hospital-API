using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Update_Map_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordinatesInsideFloor",
                table: "MapRooms");

            migrationBuilder.DropColumn(
                name: "Shape",
                table: "MapRooms");

            migrationBuilder.DropColumn(
                name: "Shape",
                table: "MapFloors");

            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "MapBuildings");

            migrationBuilder.DropColumn(
                name: "Shape",
                table: "MapBuildings");

            migrationBuilder.AddColumn<float>(
                name: "Area",
                table: "Rooms",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "MapRooms",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "MapRooms",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "XCoordinate",
                table: "MapRooms",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "YCoordinate",
                table: "MapRooms",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "MapFloors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "MapFloors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "XCoordinate",
                table: "MapFloors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "YCoordinate",
                table: "MapFloors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "MapBuildings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "MapBuildings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "XCoordinate",
                table: "MapBuildings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "YCoordinate",
                table: "MapBuildings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Area",
                table: "Floors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Floors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Buildings",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "MapRooms");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "MapRooms");

            migrationBuilder.DropColumn(
                name: "XCoordinate",
                table: "MapRooms");

            migrationBuilder.DropColumn(
                name: "YCoordinate",
                table: "MapRooms");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "MapFloors");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "MapFloors");

            migrationBuilder.DropColumn(
                name: "XCoordinate",
                table: "MapFloors");

            migrationBuilder.DropColumn(
                name: "YCoordinate",
                table: "MapFloors");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "MapBuildings");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "MapBuildings");

            migrationBuilder.DropColumn(
                name: "XCoordinate",
                table: "MapBuildings");

            migrationBuilder.DropColumn(
                name: "YCoordinate",
                table: "MapBuildings");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Buildings");

            migrationBuilder.AddColumn<string>(
                name: "CoordinatesInsideFloor",
                table: "MapRooms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shape",
                table: "MapRooms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shape",
                table: "MapFloors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "MapBuildings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shape",
                table: "MapBuildings",
                type: "text",
                nullable: true);
        }
    }
}
