using FxStreetDeveloper.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FxStreetDeveloper.DataAccess
{
    public class FxStreetDeveloperContext : DbContext
    {
        public FxStreetDeveloperContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<MatchsPlayersHouse> MatchsPlayersHouse { get; set; }
        public DbSet<MatchsPlayersAway> MatchsPlayersAway { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}