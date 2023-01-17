using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class RemoveMapRoomId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MapRooms",
                table: "MapRooms");

            migrationBuilder.DropIndex(
                name: "IX_MapRooms_RoomId",
                table: "MapRooms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MapRooms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapRooms",
                table: "MapRooms",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MapRooms",
                table: "MapRooms");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MapRooms",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapRooms",
                table: "MapRooms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MapRooms_RoomId",
                table: "MapRooms",
                column: "RoomId");
        }
    }
}
