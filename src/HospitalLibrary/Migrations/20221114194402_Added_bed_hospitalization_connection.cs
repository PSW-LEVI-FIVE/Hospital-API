using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Added_bed_hospitalization_connection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BedId1",
                table: "Hospitalizations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Beds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_BedId1",
                table: "Hospitalizations",
                column: "BedId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitalizations_Beds_BedId1",
                table: "Hospitalizations",
                column: "BedId1",
                principalTable: "Beds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospitalizations_Beds_BedId1",
                table: "Hospitalizations");

            migrationBuilder.DropIndex(
                name: "IX_Hospitalizations_BedId1",
                table: "Hospitalizations");

            migrationBuilder.DropColumn(
                name: "BedId1",
                table: "Hospitalizations");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Beds");
        }
    }
}
