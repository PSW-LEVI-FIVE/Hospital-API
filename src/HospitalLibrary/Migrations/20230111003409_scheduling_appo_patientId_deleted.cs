using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class scheduling_appo_patientId_deleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchedulingAppointmentDomainEvents_Patients_PatientId",
                table: "SchedulingAppointmentDomainEvents");

            migrationBuilder.DropIndex(
                name: "IX_SchedulingAppointmentDomainEvents_PatientId",
                table: "SchedulingAppointmentDomainEvents");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "SchedulingAppointmentDomainEvents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "SchedulingAppointmentDomainEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingAppointmentDomainEvents_PatientId",
                table: "SchedulingAppointmentDomainEvents",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchedulingAppointmentDomainEvents_Patients_PatientId",
                table: "SchedulingAppointmentDomainEvents",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
