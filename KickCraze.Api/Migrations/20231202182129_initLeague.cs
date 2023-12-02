using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KickCraze.Api.Migrations
{
    /// <inheritdoc />
    public partial class initLeague : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LeagueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeagueEmblemURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueID);
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "LeagueID", "LeagueEmblemURL", "LeagueName" },
                values: new object[,]
                {
                    { 2002, "https://crests.football-data.org/BL1.png", "Bundesliga" },
                    { 2014, "https://crests.football-data.org/PD.png", "Primera Division" },
                    { 2015, "https://crests.football-data.org/FL1.png", "Ligue 1" },
                    { 2019, "https://crests.football-data.org/SA.png", "Serie A" },
                    { 2021, "https://crests.football-data.org/PL.png", "Premier League" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
