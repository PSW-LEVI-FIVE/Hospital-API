using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class scheduling_appo_DE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Floors");

            migrationBuilder.AddColumn<float>(
                name: "Area_Measure",
                table: "Rooms",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Area_Measure",
                table: "Floors",
                type: "real",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchedulingAppointmentDomainEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingAppointmentDomainEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulingAppointmentDomainEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchedulingAppointmentDomainEvents_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingAppointmentDomainEvents_PatientId",
                table: "SchedulingAppointmentDomainEvents",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulingAppointmentDomainEvents");

            migrationBuilder.DropColumn(
                name: "Area_Measure",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Area_Measure",
                table: "Floors");

            migrationBuilder.AddColumn<float>(
                name: "Area",
                table: "Rooms",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Area",
                table: "Floors",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
