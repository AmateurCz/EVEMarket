using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SQLite;
using EVEMarket.Model;
using EveType = EVEMarket.Model.Type;

namespace EVEMarket.WPF.Data
{
    public class EveDbContext : DbContext
    {
        protected static string ConnectionString =>
            new SQLiteConnectionStringBuilder()
            {
                DataSource = Properties.Settings.Default.StaticDataDBLocation,
                ForeignKeys = true
            }.ConnectionString;

        public DbSet<MarketGroup> MarketGroups { get; set; }

        public DbSet<EveType> Types { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Constellation> Constellations { get; set; }

        public DbSet<SolarSystem> SolarSystems { get; set; }

        public DbSet<UniqueName> UniqueNames { get; set; }

        public DbSet<Icon> Icons { get; set; }

        public DbSet<Graphic> Graphics { get; set; }

        public EveDbContext() :
           base(new SQLiteConnection() { ConnectionString = ConnectionString }, true)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            BuildIcon(modelBuilder);
            BuildGraphic(modelBuilder);
            BuildUniqueNames(modelBuilder);
            BuildType(modelBuilder);
            BuildMarketGroup(modelBuilder);
            BuildSolarSystem(modelBuilder);
            BuildConstellaiton(modelBuilder);
            BuildRegion(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void BuildUniqueNames(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UniqueName>()
                .ToTable("invUniqueNames")
                .HasKey(u => u.Id);

            entity.Property(u => u.Id).HasColumnName("itemID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            entity.Property(u => u.Name).HasColumnName("itemName");
            entity.Property(u => u.GroupId).HasColumnName("groupID");
        }

        private void BuildIcon(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Icon>()
                .ToTable("eveIcons")
                .HasKey(u => u.Id);

            entity.Property(u => u.Id).HasColumnName("iconID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            entity.Property(u => u.File).HasColumnName("iconFile");
            entity.Property(u => u.Description).HasColumnName("description");
        }

        private void BuildGraphic(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Graphic>()
                .ToTable("eveGraphics")
                .HasKey(u => u.Id);

            entity.Property(u => u.Id).HasColumnName("graphicID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            entity.Property(u => u.File).HasColumnName("graphicFile");
            entity.Property(u => u.IconFolder).HasColumnName("iconFolder");
            entity.Property(u => u.FactionName).HasColumnName("softFactionName");
            entity.Property(u => u.HullName).HasColumnName("softHullName");
            entity.Property(u => u.RaceName).HasColumnName("softRaceName");
            entity.Property(u => u.Description).HasColumnName("description");
        }

        private void BuildConstellaiton(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Constellation>()
                .ToTable("mapConstellations");

            entity.Property(u => u.Id).HasColumnName("constellationID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            entity.Property(u => u.Name).HasColumnName("constellationName");
            entity.Property(u => u.RegionId).HasColumnName("regionID");
            entity.Property(u => u.Radius).HasColumnName("radius");
            entity.Property(u => u.FactionId).HasColumnName("factionID");

            // navigational properties
            entity.HasRequired(u => u.Region).WithMany(x => x.Constellations).HasForeignKey(u => u.RegionId);
        }

        private void BuildSolarSystem(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<SolarSystem>()
                .ToTable("mapSolarSystems");

            entity.Property(u => u.Id).HasColumnName("solarSystemID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            entity.Property(u => u.Name).HasColumnName("solarSystemName");
            entity.Property(u => u.RegionId).HasColumnName("regionID");
            entity.Property(u => u.ConstellationId).HasColumnName("constellationID");
            entity.Property(u => u.Security).HasColumnName("security");
            entity.Property(u => u.SecurityClass).HasColumnName("securityClass");

            // navigational properties
            entity.HasRequired(u => u.Region).WithMany(x => x.SolarSystems).HasForeignKey(u => u.RegionId);
            entity.HasRequired(u => u.Constellation).WithMany(x => x.SolarSystems).HasForeignKey(u => u.ConstellationId);
        }

        private void BuildRegion(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Region>()
                   .ToTable("mapRegions")
                   .HasKey(u => u.Id);

            entity.Property(u => u.Id).HasColumnName("regionID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            entity.Property(u => u.Name).HasColumnName("regionName");
            entity.Property(u => u.FactionId).HasColumnName("factionID");
            entity.Property(u => u.NameId).HasColumnName("nameID");
            entity.Property(u => u.DescriptionId).HasColumnName("descriptionID");
        }

        private void BuildType(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<EveType>()
                .ToTable("invTypes")
                .HasKey(u => u.Id)
                .Ignore(u => u.Masteries)
                .Ignore(u => u.Name)
                .Ignore(u => u.Description);

            entity.Property(u => u.Id).HasColumnName("typeID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            entity.Property(u => u.GroupId).HasColumnName("groupID");
            entity.Property(u => u.MarketGroupId).HasColumnName("marketGroupID");
            entity.Property(u => u.FactionId).HasColumnName("factionID");
            entity.Property(u => u.NameDb).HasColumnName("typeName");
            entity.Property(u => u.DescriptionDb).HasColumnName("description");
            entity.Property(u => u.FactionName).HasColumnName("sofFactionName");
            entity.Property(u => u.PortionSize).HasColumnName("portionSize");
            entity.Property(u => u.RaceId).HasColumnName("raceID");
            entity.Property(u => u.BasePrice).HasColumnName("basePrice");
            entity.Property(u => u.Capacity).HasColumnName("capacity");
            entity.Property(u => u.Volume).HasColumnName("volume");
            entity.Property(u => u.Radius).HasColumnName("radius");
            entity.Property(u => u.SoundId).HasColumnName("soundID");
            entity.Property(u => u.IconId).HasColumnName("iconID");
            entity.Property(u => u.GraphicId).HasColumnName("graphicID");
            entity.Property(u => u.MaterialSetId).HasColumnName("sofMaterialSetID");
            entity.Property(u => u.Published).HasColumnName("published");

            // navigational properties
            entity.HasOptional(u => u.MarketGroup).WithMany(x => x.ChildTypes).HasForeignKey(u => u.MarketGroupId);
            entity.HasOptional(u => u.Icon).WithMany(x => x.AttachedTypes).HasForeignKey(u => u.IconId);
            entity.HasOptional(u => u.Graphic).WithMany(x => x.AttachedTypes).HasForeignKey(u => u.GraphicId);
        }

        private void BuildMarketGroup(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<MarketGroup>()
                   .ToTable("invMarketGroups")
                   .HasKey(u => u.Id);

            entity.Property(u => u.Id).HasColumnName("marketGroupId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            entity.Property(u => u.Name).HasColumnName("marketGroupName");
            entity.Property(u => u.IconId).HasColumnName("iconID");
            entity.Property(u => u.Description).HasColumnName("description");
            entity.Property(u => u.HasTypes).HasColumnName("hasTypes");
            entity.Property(u => u.ParentMarketGroupId).HasColumnName("parentGroupID");

            // navigational properties
            entity.HasOptional(u => u.ParentMarketGroup).WithMany(x => x.ChildMarketGroups).HasForeignKey(u => u.ParentMarketGroupId);
        }
    }
}