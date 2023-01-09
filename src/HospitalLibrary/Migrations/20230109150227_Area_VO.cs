using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Area_VO : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
