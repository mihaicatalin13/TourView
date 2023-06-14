using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourView.Data.Migrations
{
    public partial class addmig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMan",
                table: "Locations");

            migrationBuilder.AddColumn<string>(
                name: "ProprietarId",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProprietarId",
                table: "Locations");

            migrationBuilder.AddColumn<string>(
                name: "IdMan",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
