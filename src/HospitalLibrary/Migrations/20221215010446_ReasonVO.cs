using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class ReasonVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "BloodOrders",
                newName: "Reason_Text");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "AnnualLeaves",
                newName: "Reason_Text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reason_Text",
                table: "BloodOrders",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "Reason_Text",
                table: "AnnualLeaves",
                newName: "Reason");
        }
    }
}
