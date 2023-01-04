using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class Added_domain_event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ExaminationReports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DomainEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AggregateId = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExaminationReportId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainEvent_ExaminationReports_ExaminationReportId",
                        column: x => x.ExaminationReportId,
                        principalTable: "ExaminationReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationReportDomainEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationReportDomainEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationReportDomainEvents_DomainEvent_Id",
                        column: x => x.Id,
                        principalTable: "DomainEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_ExaminationReportId",
                table: "DomainEvent",
                column: "ExaminationReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationReportDomainEvents");

            migrationBuilder.DropTable(
                name: "DomainEvent");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ExaminationReports");
        }
    }
}
