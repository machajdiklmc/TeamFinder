using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Data.Migrations
{
    public partial class joinedUserEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "JoinedUsersId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_JoinedUsersId",
                table: "Events",
                column: "JoinedUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_JoinedUsersId",
                table: "Events",
                column: "JoinedUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_JoinedUsersId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_JoinedUsersId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "JoinedUsersId",
                table: "Events");

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
    }
}
