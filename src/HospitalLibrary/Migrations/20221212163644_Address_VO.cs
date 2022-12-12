using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class Address_VO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Persons",
                newName: "Address_StreetNumber");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Persons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Persons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Persons",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Address_StreetNumber",
                table: "Persons",
                newName: "Address");
        }
    }
}
