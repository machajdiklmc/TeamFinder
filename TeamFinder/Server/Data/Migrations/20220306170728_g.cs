using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFinder.Server.Data.Migrations
{
    public partial class g : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
