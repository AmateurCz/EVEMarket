using System;
using System.Collections.Generic;
using System.Text;
using EVEMarket.Model;
using EveType = EVEMarket.Model.Type;

namespace EVEMarket.DataProviders
{
    public interface IStaticData
    {
        List<MarketGroup> MarketGroups { get; }

        List<MarketGroup> MarketGroupTree { get; }

        Dictionary<int, EveType> Types { get; }
    }
}
