using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Data.Migrations
{
    public partial class joinedUsersEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SportEventId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SportEventId",
                table: "AspNetUsers",
                column: "SportEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Events_SportEventId",
                table: "AspNetUsers",
                column: "SportEventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Events_SportEventId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SportEventId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SportEventId",
                table: "AspNetUsers");
        }
    }
}
