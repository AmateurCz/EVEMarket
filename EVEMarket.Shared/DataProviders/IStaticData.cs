using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EVEMarket.Model;
using EveType = EVEMarket.Model.Type;

namespace EVEMarket.DataProviders
{
    public interface IStaticData
    {
        IQueryable<Region> Regions { get; }

        IQueryable<MarketGroup> MarketGroups { get; }
        
        IQueryable<EveType> Types { get; }

        IQueryable<Constellation> Constellations { get; }

        IQueryable<SolarSystem> SolarSystems { get; }

        IQueryable<UniqueName> UniqueNames { get; }
    }
}
