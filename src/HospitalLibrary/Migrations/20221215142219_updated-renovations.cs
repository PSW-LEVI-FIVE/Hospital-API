using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class updatedrenovations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Renovations_Rooms_SecondaryRoomId",
                table: "Renovations");

            migrationBuilder.AlterColumn<int>(
                name: "SecondaryRoomId",
                table: "Renovations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Renovations_Rooms_SecondaryRoomId",
                table: "Renovations",
                column: "SecondaryRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Renovations_Rooms_SecondaryRoomId",
                table: "Renovations");

            migrationBuilder.AlterColumn<int>(
                name: "SecondaryRoomId",
                table: "Renovations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Renovations_Rooms_SecondaryRoomId",
                table: "Renovations",
                column: "SecondaryRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
