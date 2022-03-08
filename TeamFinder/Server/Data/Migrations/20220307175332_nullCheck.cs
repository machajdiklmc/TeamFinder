using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Data.Migrations
{
    public partial class nullCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JoinedEventsId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "JoinedEventsId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JoinedEventsId",
                table: "AspNetUsers",
                column: "JoinedEventsId",
                unique: true,
                filter: "[JoinedEventsId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JoinedEventsId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "JoinedEventsId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JoinedEventsId",
                table: "AspNetUsers",
                column: "JoinedEventsId",
                unique: true);
        }
    }
}
