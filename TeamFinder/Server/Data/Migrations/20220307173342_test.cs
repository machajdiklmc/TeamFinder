using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_JoinedEvents_JoinedEventsId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_JoinedEventsId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "JoinedEventsId",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "SportEventId",
                table: "JoinedEvents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "JoinedEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "JoinedEventsId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SportEventId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JoinedEvents_SportEventId",
                table: "JoinedEvents",
                column: "SportEventId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JoinedEventsId",
                table: "AspNetUsers",
                column: "JoinedEventsId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_JoinedEvents_JoinedEventsId",
                table: "AspNetUsers",
                column: "JoinedEventsId",
                principalTable: "JoinedEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedEvents_Events_SportEventId",
                table: "JoinedEvents",
                column: "SportEventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Events_SportEventId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_JoinedEvents_JoinedEventsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedEvents_Events_SportEventId",
                table: "JoinedEvents");

            migrationBuilder.DropIndex(
                name: "IX_JoinedEvents_SportEventId",
                table: "JoinedEvents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JoinedEventsId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SportEventId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SportEventId",
                table: "JoinedEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JoinedEvents");

            migrationBuilder.DropColumn(
                name: "JoinedEventsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SportEventId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "JoinedEventsId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
