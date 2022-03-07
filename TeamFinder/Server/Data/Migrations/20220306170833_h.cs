using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Data.Migrations
{
    public partial class h : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_JoinedUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_JoinedUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "JoinedUserId",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JoinedUserId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Events_JoinedUserId",
                table: "Events",
                column: "JoinedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_JoinedUserId",
                table: "Events",
                column: "JoinedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
