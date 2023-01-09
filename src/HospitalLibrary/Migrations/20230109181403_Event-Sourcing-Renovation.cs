using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class EventSourcingRenovation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Renovations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RenovationId",
                table: "DomainEvent",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_RenovationId",
                table: "DomainEvent",
                column: "RenovationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainEvent_Renovations_RenovationId",
                table: "DomainEvent",
                column: "RenovationId",
                principalTable: "Renovations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainEvent_Renovations_RenovationId",
                table: "DomainEvent");

            migrationBuilder.DropIndex(
                name: "IX_DomainEvent_RenovationId",
                table: "DomainEvent");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Renovations");

            migrationBuilder.DropColumn(
                name: "RenovationId",
                table: "DomainEvent");
        }
    }
}
