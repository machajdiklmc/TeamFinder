using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Data.Migrations
{
    public partial class joinedEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JoinedEventsId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "JoinedEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_JoinedEventsId",
                table: "Events",
                column: "JoinedEventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_JoinedEvents_JoinedEventsId",
                table: "Events",
                column: "JoinedEventsId",
                principalTable: "JoinedEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_JoinedEvents_JoinedEventsId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "JoinedEvents");

            migrationBuilder.DropIndex(
                name: "IX_Events_JoinedEventsId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "JoinedEventsId",
                table: "Events");
        }
    }
}
