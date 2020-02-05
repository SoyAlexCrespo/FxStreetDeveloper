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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}