using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class Empty3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentReallocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartingRoomId = table.Column<int>(type: "integer", nullable: false),
                    DestinationRoomId = table.Column<int>(type: "integer", nullable: false),
                    EquipmentId = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    StartAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentReallocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentReallocations_RoomEquipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "RoomEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentReallocations_Rooms_DestinationRoomId",
                        column: x => x.DestinationRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentReallocations_Rooms_StartingRoomId",
                        column: x => x.StartingRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentReallocations_DestinationRoomId",
                table: "EquipmentReallocations",
                column: "DestinationRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentReallocations_EquipmentId",
                table: "EquipmentReallocations",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentReallocations_StartingRoomId",
                table: "EquipmentReallocations",
                column: "StartingRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentReallocations");
        }
    }
}
