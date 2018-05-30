using EVEMarket.Model;
using Microsoft.EntityFrameworkCore;

namespace EVEMarket.WPF.Data
{
    public class EveDbContext : DbContext
    {
        public EveDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"DataSource=C:\Users\kubatdav\Downloads\SDE\EveStaticData.sqlite;");
        }

        public DbSet<MarketGroup> MarketGroups { get; set; }

        public DbSet<Type> Types { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Constellation> Constellations { get; set; }

        public DbSet<SolarSystem> SolarSystems { get; set; }

        public DbSet<UniqueName> UniqueNames { get; set; }
    }
}
