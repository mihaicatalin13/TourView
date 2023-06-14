using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourView.Data.Migrations
{
    public partial class addmig11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProprietarId",
                table: "Locations",
                newName: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Locations",
                newName: "ProprietarId");
        }
    }
}
