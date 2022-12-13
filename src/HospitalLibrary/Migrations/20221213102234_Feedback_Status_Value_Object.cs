using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Feedback_Status_Value_Object : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "state",
                table: "Hospitalizations",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Published",
                table: "Feedbacks",
                newName: "FeedbackStatus_Published");

            migrationBuilder.RenameColumn(
                name: "Anonimity",
                table: "Feedbacks",
                newName: "FeedbackStatus_Anonimity");

            migrationBuilder.RenameColumn(
                name: "AllowPublishment",
                table: "Feedbacks",
                newName: "FeedbackStatus_AllowPublishment");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Appointments",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "AnnualLeaves",
                newName: "State");

            migrationBuilder.AlterColumn<bool>(
                name: "FeedbackStatus_Published",
                table: "Feedbacks",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "FeedbackStatus_Anonimity",
                table: "Feedbacks",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "FeedbackStatus_AllowPublishment",
                table: "Feedbacks",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Hospitalizations",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "FeedbackStatus_Published",
                table: "Feedbacks",
                newName: "Published");

            migrationBuilder.RenameColumn(
                name: "FeedbackStatus_Anonimity",
                table: "Feedbacks",
                newName: "Anonimity");

            migrationBuilder.RenameColumn(
                name: "FeedbackStatus_AllowPublishment",
                table: "Feedbacks",
                newName: "AllowPublishment");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Appointments",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "AnnualLeaves",
                newName: "state");

            migrationBuilder.AlterColumn<bool>(
                name: "Published",
                table: "Feedbacks",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Anonimity",
                table: "Feedbacks",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AllowPublishment",
                table: "Feedbacks",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }
    }
}
