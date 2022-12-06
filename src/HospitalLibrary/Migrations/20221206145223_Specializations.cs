using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class Specializations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecialtyType",
                table: "Doctors",
                newName: "SpecialityId");

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialityId",
                table: "Doctors",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialities_SpecialityId",
                table: "Doctors",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialities_SpecialityId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecialityId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "SpecialityId",
                table: "Doctors",
                newName: "SpecialtyType");
        }
    }
}
