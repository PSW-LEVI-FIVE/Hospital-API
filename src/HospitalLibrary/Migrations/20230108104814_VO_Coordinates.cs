using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class VO_Coordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YCoordinate",
                table: "MapRooms",
                newName: "Coordinates_YCoordinate");

            migrationBuilder.RenameColumn(
                name: "XCoordinate",
                table: "MapRooms",
                newName: "Coordinates_XCoordinate");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "MapRooms",
                newName: "Coordinates_Width");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "MapRooms",
                newName: "Coordinates_Height");

            migrationBuilder.RenameColumn(
                name: "YCoordinate",
                table: "MapFloors",
                newName: "Coordinates_YCoordinate");

            migrationBuilder.RenameColumn(
                name: "XCoordinate",
                table: "MapFloors",
                newName: "Coordinates_XCoordinate");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "MapFloors",
                newName: "Coordinates_Width");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "MapFloors",
                newName: "Coordinates_Height");

            migrationBuilder.RenameColumn(
                name: "YCoordinate",
                table: "MapBuildings",
                newName: "Coordinates_YCoordinate");

            migrationBuilder.RenameColumn(
                name: "XCoordinate",
                table: "MapBuildings",
                newName: "Coordinates_XCoordinate");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "MapBuildings",
                newName: "Coordinates_Width");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "MapBuildings",
                newName: "Coordinates_Height");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_YCoordinate",
                table: "MapRooms",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_XCoordinate",
                table: "MapRooms",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_Width",
                table: "MapRooms",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_Height",
                table: "MapRooms",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_YCoordinate",
                table: "MapFloors",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_XCoordinate",
                table: "MapFloors",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_Width",
                table: "MapFloors",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_Height",
                table: "MapFloors",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_YCoordinate",
                table: "MapBuildings",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_XCoordinate",
                table: "MapBuildings",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_Width",
                table: "MapBuildings",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Coordinates_Height",
                table: "MapBuildings",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coordinates_YCoordinate",
                table: "MapRooms",
                newName: "YCoordinate");

            migrationBuilder.RenameColumn(
                name: "Coordinates_XCoordinate",
                table: "MapRooms",
                newName: "XCoordinate");

            migrationBuilder.RenameColumn(
                name: "Coordinates_Width",
                table: "MapRooms",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "Coordinates_Height",
                table: "MapRooms",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "Coordinates_YCoordinate",
                table: "MapFloors",
                newName: "YCoordinate");

            migrationBuilder.RenameColumn(
                name: "Coordinates_XCoordinate",
                table: "MapFloors",
                newName: "XCoordinate");

            migrationBuilder.RenameColumn(
                name: "Coordinates_Width",
                table: "MapFloors",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "Coordinates_Height",
                table: "MapFloors",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "Coordinates_YCoordinate",
                table: "MapBuildings",
                newName: "YCoordinate");

            migrationBuilder.RenameColumn(
                name: "Coordinates_XCoordinate",
                table: "MapBuildings",
                newName: "XCoordinate");

            migrationBuilder.RenameColumn(
                name: "Coordinates_Width",
                table: "MapBuildings",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "Coordinates_Height",
                table: "MapBuildings",
                newName: "Height");

            migrationBuilder.AlterColumn<float>(
                name: "YCoordinate",
                table: "MapRooms",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "XCoordinate",
                table: "MapRooms",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Width",
                table: "MapRooms",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Height",
                table: "MapRooms",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "YCoordinate",
                table: "MapFloors",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "XCoordinate",
                table: "MapFloors",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Width",
                table: "MapFloors",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Height",
                table: "MapFloors",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "YCoordinate",
                table: "MapBuildings",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "XCoordinate",
                table: "MapBuildings",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Width",
                table: "MapBuildings",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Height",
                table: "MapBuildings",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
