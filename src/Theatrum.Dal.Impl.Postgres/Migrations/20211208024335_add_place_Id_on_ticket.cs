using Microsoft.EntityFrameworkCore.Migrations;

namespace Theatrum.Dal.Impl.Postgres.Migrations
{
    public partial class add_place_Id_on_ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceId",
                table: "Tickets",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Tickets");
        }
    }
}
