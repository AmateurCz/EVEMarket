using EVEMarket.Model;
using Microsoft.EntityFrameworkCore;

namespace EVEMarket.WPF.Data
{
    public class EveDbContext : DbContext
    {
        protected string ConnectionString => $"DataSource={Properties.Settings.Default.StaticDataDBLocation};"


        public DbSet<MarketGroup> MarketGroups { get; set; }

        public DbSet<Type> Types { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Constellation> Constellations { get; set; }

        public DbSet<SolarSystem> SolarSystems { get; set; }

        public DbSet<UniqueName> UniqueNames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }
}