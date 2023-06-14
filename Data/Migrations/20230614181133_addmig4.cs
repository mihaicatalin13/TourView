using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourView.Data.Migrations
{
    public partial class addmig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdManager",
                table: "Locations",
                newName: "IdMan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdMan",
                table: "Locations",
                newName: "IdManager");
        }
    }
}
