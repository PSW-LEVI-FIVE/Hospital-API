using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class Added_Prescriptions_Symptoms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExaminationReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ExaminationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationReports_Appointments_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationReports_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicineId = table.Column<int>(type: "integer", nullable: false),
                    Dose = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "ExaminationReportSymptom",
                columns: table => new
                {
                    ExaminationReportsId = table.Column<int>(type: "integer", nullable: false),
                    SymptomsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationReportSymptom", x => new { x.ExaminationReportsId, x.SymptomsId });
                    table.ForeignKey(
                        name: "FK_ExaminationReportSymptom_ExaminationReports_ExaminationRepo~",
                        column: x => x.ExaminationReportsId,
                        principalTable: "ExaminationReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationReportSymptom_Symptoms_SymptomsId",
                        column: x => x.SymptomsId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationReportPrescription_PrescriptionsId",
                table: "ExaminationReportPrescription",
                column: "PrescriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationReports_DoctorId",
                table: "ExaminationReports",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationReports_ExaminationId",
                table: "ExaminationReports",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationReportSymptom_SymptomsId",
                table: "ExaminationReportSymptom",
                column: "SymptomsId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions",
                column: "MedicineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationReportPrescription");

            migrationBuilder.DropTable(
                name: "ExaminationReportSymptom");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "ExaminationReports");

            migrationBuilder.DropTable(
                name: "Symptoms");
        }
    }
}
