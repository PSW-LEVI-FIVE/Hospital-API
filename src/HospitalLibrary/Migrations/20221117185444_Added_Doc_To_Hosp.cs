using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Added_Doc_To_Hosp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Therapies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Medicines",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Therapies_DoctorId",
                table: "Therapies",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Therapies_Doctors_DoctorId",
                table: "Therapies",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Therapies_Doctors_DoctorId",
                table: "Therapies");

            migrationBuilder.DropIndex(
                name: "IX_Therapies_DoctorId",
                table: "Therapies");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Therapies");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Medicines");
        }
    }
}
