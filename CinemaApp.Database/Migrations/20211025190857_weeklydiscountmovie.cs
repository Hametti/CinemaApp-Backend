using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaApp.Database.Migrations
{
    public partial class weeklydiscountmovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeeklyDiscountMovie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeeklyDiscountId = table.Column<int>(type: "int", nullable: true),
                    WeeklyDiscountValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyDiscountMovie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeklyDiscountMovie_Movies_WeeklyDiscountId",
                        column: x => x.WeeklyDiscountId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyDiscountMovie_WeeklyDiscountId",
                table: "WeeklyDiscountMovie",
                column: "WeeklyDiscountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeeklyDiscountMovie");
        }
    }
}
