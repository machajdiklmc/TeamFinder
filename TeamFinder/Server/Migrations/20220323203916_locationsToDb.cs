using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Migrations
{
    public partial class locationsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_SportEventLocation_LocationId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SportEventLocation",
                table: "SportEventLocation");

            migrationBuilder.RenameTable(
                name: "SportEventLocation",
                newName: "Locations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "SportEventLocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SportEventLocation",
                table: "SportEventLocation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_SportEventLocation_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "SportEventLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
