using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourView.Data.Migrations
{
    public partial class ReservationSeen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "seen",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "seen",
                table: "Reservations");
        }
    }
}
