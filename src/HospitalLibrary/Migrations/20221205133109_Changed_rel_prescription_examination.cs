using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Changed_rel_prescription_examination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationReportPrescription");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationReportId",
                table: "Prescriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ExaminationReportId",
                table: "Prescriptions",
                column: "ExaminationReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_ExaminationReports_ExaminationReportId",
                table: "Prescriptions",
                column: "ExaminationReportId",
                principalTable: "ExaminationReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_ExaminationReports_ExaminationReportId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_ExaminationReportId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "ExaminationReportId",
                table: "Prescriptions");

            migrationBuilder.CreateTable(
                name: "ExaminationReportPrescription",
                columns: table => new
                {
                    ExaminationReportsId = table.Column<int>(type: "integer", nullable: false),
                    PrescriptionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationReportPrescription", x => new { x.ExaminationReportsId, x.PrescriptionsId });
                    table.ForeignKey(
                        name: "FK_ExaminationReportPrescription_ExaminationReports_Examinatio~",
                        column: x => x.ExaminationReportsId,
                        principalTable: "ExaminationReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationReportPrescription_Prescriptions_PrescriptionsId",
                        column: x => x.PrescriptionsId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationReportPrescription_PrescriptionsId",
                table: "ExaminationReportPrescription",
                column: "PrescriptionsId");
        }
    }
}
