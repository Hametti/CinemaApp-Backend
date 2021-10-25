using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaApp.Database.Migrations
{
    public partial class addedusercreds2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserCreds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCreds_UserId",
                table: "UserCreds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCreds_Users_UserId",
                table: "UserCreds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCreds_Users_UserId",
                table: "UserCreds");

            migrationBuilder.DropIndex(
                name: "IX_UserCreds_UserId",
                table: "UserCreds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserCreds");
        }
    }
}
