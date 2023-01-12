using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Renovation_Merge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Renovations_Rooms_SecondaryRoomId",
                table: "Renovations");

            migrationBuilder.DropIndex(
                name: "IX_Renovations_SecondaryRoomId",
                table: "Renovations");

            migrationBuilder.DropColumn(
                name: "SecondaryRoomId",
                table: "Renovations");

            migrationBuilder.AddColumn<string>(
                name: "SecondaryRoomIds",
                table: "Renovations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondaryRoomIds",
                table: "Renovations");

            migrationBuilder.AddColumn<int>(
                name: "SecondaryRoomId",
                table: "Renovations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Renovations_SecondaryRoomId",
                table: "Renovations",
                column: "SecondaryRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Renovations_Rooms_SecondaryRoomId",
                table: "Renovations",
                column: "SecondaryRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
