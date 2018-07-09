using System.Data.Entity;
using System.Linq;
using EVEMarket.Data.Providers;
using EVEMarket.Model;
using NLog;
using EveType = EVEMarket.Model.Type;

namespace EVEMarket.WPF.Data.Providers
{
    internal class StaticDb : IStaticData
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private EveDbContext _context;

        private EveDbContext Context
        {
            get
            {
                if (_context == null)
                {
                    Database.SetInitializer<EveDbContext>(null);
                    _context = new EveDbContext();
                    _context.Configuration.LazyLoadingEnabled = false;
                    _context.Configuration.AutoDetectChangesEnabled = false;
                    _context.Database.Log = WriteLog;
                }
                return _context;
            }
        }

        private void WriteLog(string log)
        {
            logger.Info(log);
        }

        public IQueryable<MarketGroup> MarketGroups => Context.MarketGroups;

        public IQueryable<EveType> Types => Context.Types.Where(x => x.Published);

        public IQueryable<Region> Regions => Context.Regions;

        public IQueryable<Constellation> Constellations => Context.Constellations;

        public IQueryable<SolarSystem> SolarSystems => Context.SolarSystems;

        public IQueryable<UniqueName> UniqueNames => Context.UniqueNames;
    }
}