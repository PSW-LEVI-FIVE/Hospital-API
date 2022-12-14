using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class QuantityAsVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BloodOrders");

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

            migrationBuilder.AddColumn<double>(
                name: "Quantity_Count",
                table: "BloodOrders",
                type: "double precision",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity_Count",
                table: "BloodOrders");

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

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "BloodOrders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
