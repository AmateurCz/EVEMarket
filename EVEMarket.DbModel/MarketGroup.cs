using System.Collections.Generic;

namespace EVEMarket.Model
{
    public class MarketGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? IconId { get; set; }

        public string Description { get; set; }

        public bool HasTypes { get; set; }

        public int? ParentMarketGroupId { get; set; }

        public virtual MarketGroup ParentMarketGroup { get; set; }

        public virtual List<MarketGroup> ChildMarketGroups { get; set; } = new List<MarketGroup>();

        public virtual List<Type> ChildTypes { get; set; } = new List<Type>();
    }
}