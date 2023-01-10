using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class appointment_aggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "DomainEvent",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_AppointmentId",
                table: "DomainEvent",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainEvent_Appointments_AppointmentId",
                table: "DomainEvent",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainEvent_Appointments_AppointmentId",
                table: "DomainEvent");

            migrationBuilder.DropIndex(
                name: "IX_DomainEvent_AppointmentId",
                table: "DomainEvent");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "DomainEvent");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Appointments");
        }
    }
}
