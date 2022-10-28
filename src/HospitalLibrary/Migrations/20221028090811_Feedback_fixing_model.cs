using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Feedback_fixing_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackStatus",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "Publishment",
                table: "Feedbacks",
                newName: "Published");

            migrationBuilder.AddColumn<bool>(
                name: "AllowPublishment",
                table: "Feedbacks",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Anonimity",
                table: "Feedbacks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PatientId",
                table: "Feedbacks",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Patients_PatientId",
                table: "Feedbacks",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Patients_PatientId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_PatientId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "AllowPublishment",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Anonimity",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "Published",
                table: "Feedbacks",
                newName: "Publishment");

            migrationBuilder.AddColumn<int>(
                name: "FeedbackStatus",
                table: "Feedbacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
