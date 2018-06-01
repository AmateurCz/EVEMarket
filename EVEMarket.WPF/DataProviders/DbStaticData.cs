using System.Linq;
using EVEMarket.DataProviders;
using EVEMarket.Model;
using EVEMarket.WPF.Data;
using EveType = EVEMarket.Model.Type;

namespace EVEMarket.WPF.DataProviders
{
    internal class DbStaticData : IStaticData
    {
        private EveDbContext _context;

        private EveDbContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new EveDbContext();
                }
                return _context;
            }
        }

        public IQueryable<MarketGroup> MarketGroups => Context.MarketGroups;

        public IQueryable<EveType> Types => Context.Types.Where(x => x.Published);

        public IQueryable<Region> Regions => Context.Regions;

        public IQueryable<Constellation> Constellations => Context.Constellations;

        public IQueryable<SolarSystem> SolarSystems => Context.SolarSystems;

        public IQueryable<UniqueName> UniqueNames => Context.UniqueNames;
    }
}