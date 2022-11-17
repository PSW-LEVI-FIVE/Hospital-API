using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class AddedSeccondRoomToEquipReall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentReallocations_Rooms_RoomId",
                table: "EquipmentReallocations");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "EquipmentReallocations",
                newName: "StartingRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentReallocations_RoomId",
                table: "EquipmentReallocations",
                newName: "IX_EquipmentReallocations_StartingRoomId");

            migrationBuilder.AddColumn<int>(
                name: "DestinationRoomId",
                table: "EquipmentReallocations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentReallocations_DestinationRoomId",
                table: "EquipmentReallocations",
                column: "DestinationRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentReallocations_Rooms_DestinationRoomId",
                table: "EquipmentReallocations",
                column: "DestinationRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentReallocations_Rooms_StartingRoomId",
                table: "EquipmentReallocations",
                column: "StartingRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentReallocations_Rooms_DestinationRoomId",
                table: "EquipmentReallocations");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentReallocations_Rooms_StartingRoomId",
                table: "EquipmentReallocations");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentReallocations_DestinationRoomId",
                table: "EquipmentReallocations");

            migrationBuilder.DropColumn(
                name: "DestinationRoomId",
                table: "EquipmentReallocations");

            migrationBuilder.RenameColumn(
                name: "StartingRoomId",
                table: "EquipmentReallocations",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentReallocations_StartingRoomId",
                table: "EquipmentReallocations",
                newName: "IX_EquipmentReallocations_RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentReallocations_Rooms_RoomId",
                table: "EquipmentReallocations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
