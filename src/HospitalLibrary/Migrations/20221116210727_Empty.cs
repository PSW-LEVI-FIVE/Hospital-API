using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Empty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "BedId",
                table: "Hospitalizations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_BedId",
                table: "Hospitalizations",
                column: "BedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitalizations_Beds_BedId",
                table: "Hospitalizations",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospitalizations_Beds_BedId",
                table: "Hospitalizations");

            migrationBuilder.DropIndex(
                name: "IX_Hospitalizations_BedId",
                table: "Hospitalizations");

            migrationBuilder.DropColumn(
                name: "BedId",
                table: "Hospitalizations");

            migrationBuilder.AddColumn<int>(
                name: "BedId1",
                table: "Hospitalizations",
                type: "integer",
                nullable: true);

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
    }
}
