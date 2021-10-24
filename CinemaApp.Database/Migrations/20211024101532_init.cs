using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaApp.Database.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Budget = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShowingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowingHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyViewMovie",
                columns: table => new
                {
                    DailyViewListId = table.Column<int>(type: "int", nullable: false),
                    MovieListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyViewMovie", x => new { x.DailyViewListId, x.MovieListId });
                    table.ForeignKey(
                        name: "FK_DailyViewMovie_DailyViews_DailyViewListId",
                        column: x => x.DailyViewListId,
                        principalTable: "DailyViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyViewMovie_Movies_MovieListId",
                        column: x => x.MovieListId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieShowingHour",
                columns: table => new
                {
                    MovieListId = table.Column<int>(type: "int", nullable: false),
                    ShowingHourListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieShowingHour", x => new { x.MovieListId, x.ShowingHourListId });
                    table.ForeignKey(
                        name: "FK_MovieShowingHour_Movies_MovieListId",
                        column: x => x.MovieListId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieShowingHour_ShowingHours_ShowingHourListId",
                        column: x => x.ShowingHourListId,
                        principalTable: "ShowingHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyViewMovie_MovieListId",
                table: "DailyViewMovie",
                column: "MovieListId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieShowingHour_ShowingHourListId",
                table: "MovieShowingHour",
                column: "ShowingHourListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyViewMovie");

            migrationBuilder.DropTable(
                name: "MovieShowingHour");

            migrationBuilder.DropTable(
                name: "DailyViews");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "ShowingHours");
        }
    }
}
