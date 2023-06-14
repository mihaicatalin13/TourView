using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourView.Data.Migrations
{
    public partial class addmig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Locations",
                newName: "IdManager");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdManager",
                table: "Locations",
                newName: "ManagerId");
        }
    }
}
