using System.Data.Entity;
using System.Data.SQLite;
using EVEMarket.Model;

namespace EVEMarket.WPF.Data
{
    public class EveDbContext : DbContext
    {
        protected static string ConnectionString =>
            new SQLiteConnectionStringBuilder() {
                DataSource = Properties.Settings.Default.StaticDataDBLocation,
                ForeignKeys = true }.ConnectionString;


        public EveDbContext() :
           base(new SQLiteConnection()
           {
               ConnectionString = ConnectionString
           }, true)
        {
        }

        public DbSet<MarketGroup> MarketGroups { get; set; }

        public DbSet<Type> Types { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Constellation> Constellations { get; set; }

        public DbSet<SolarSystem> SolarSystems { get; set; }

        public DbSet<UniqueName> UniqueNames { get; set; }
    }
}