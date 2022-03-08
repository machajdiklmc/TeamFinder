using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Data.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_JoinedEvents_JoinedEventsId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JoinedEventsId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "JoinedEvents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_JoinedEvents_UserId",
                table: "JoinedEvents",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedEvents_AspNetUsers_UserId",
                table: "JoinedEvents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinedEvents_AspNetUsers_UserId",
                table: "JoinedEvents");

            migrationBuilder.DropIndex(
                name: "IX_JoinedEvents_UserId",
                table: "JoinedEvents");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "JoinedEvents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JoinedEventsId",
                table: "AspNetUsers",
                column: "JoinedEventsId",
                unique: true,
                filter: "[JoinedEventsId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_JoinedEvents_JoinedEventsId",
                table: "AspNetUsers",
                column: "JoinedEventsId",
                principalTable: "JoinedEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
