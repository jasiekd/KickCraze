using KickCraze.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KickCraze.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<LeaguesEntity> Leagues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<EventsEntity>()
            //    .HasOne(f => f.Type)
            //    .WithMany()
            //    .HasForeignKey(f => f.TypeID);

            modelBuilder.Entity<LeaguesEntity>().HasData(
                new LeaguesEntity
                {
                    LeagueID = 2021,
                    LeagueName = "Premier League",
                    LeagueEmblemURL = "https://crests.football-data.org/PL.png",
                },
                new LeaguesEntity
                {
                    LeagueID = 2015,
                    LeagueName = "Ligue 1",
                    LeagueEmblemURL = "https://crests.football-data.org/FL1.png",
                },
                new LeaguesEntity
                {
                    LeagueID = 2002,
                    LeagueName = "Bundesliga",
                    LeagueEmblemURL = "https://crests.football-data.org/BL1.png",
                },
                new LeaguesEntity
                {
                    LeagueID = 2019,
                    LeagueName = "Serie A",
                    LeagueEmblemURL = "https://crests.football-data.org/SA.png",
                },
                new LeaguesEntity
                {
                    LeagueID = 2014,
                    LeagueName = "Primera Division",
                    LeagueEmblemURL = "https://crests.football-data.org/PD.png",
                }
            );
        }
    }
}
