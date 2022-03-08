using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Data.Migrations
{
    public partial class userEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Events_SportEventId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinedEvents",
                table: "JoinedEvents");

            migrationBuilder.DropIndex(
                name: "IX_JoinedEvents_UserId",
                table: "JoinedEvents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SportEventId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JoinedEvents");

            migrationBuilder.DropColumn(
                name: "JoinedEventsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SportEventId",
                table: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinedEvents",
                table: "JoinedEvents",
                columns: new[] { "UserId", "SportEventId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinedEvents",
                table: "JoinedEvents");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "JoinedEvents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "JoinedEventsId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SportEventId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinedEvents",
                table: "JoinedEvents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JoinedEvents_UserId",
                table: "JoinedEvents",
                column: "UserId",
                unique: true);

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
