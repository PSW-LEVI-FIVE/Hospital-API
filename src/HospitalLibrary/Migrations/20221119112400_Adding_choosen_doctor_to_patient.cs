using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Adding_choosen_doctor_to_patient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChoosenDoctorId",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ChoosenDoctorId",
                table: "Patients",
                column: "ChoosenDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_ChoosenDoctorId",
                table: "Patients",
                column: "ChoosenDoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_ChoosenDoctorId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ChoosenDoctorId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ChoosenDoctorId",
                table: "Patients");
        }
    }
}
