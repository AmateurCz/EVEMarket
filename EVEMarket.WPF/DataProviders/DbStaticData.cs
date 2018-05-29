using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVEMarket.DataProviders;
using EVEMarket.Model;
using EVEMarket.WPF.Data;
using EveType = EVEMarket.Model.Type;

namespace EVEMarket.WPF.DataProviders
{
    class DbStaticData : IStaticData
    {
        private EveDbContext _context;
        private List<MarketGroup> _marketGroups;
        private List<MarketGroup> _marketGroupTree;
        private Dictionary<int, EveType> _types;

        private EveDbContext Context
        {
            get
            {
                if (_context == null)
                {
                    SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                    _context = new EveDbContext();
                }
                return _context;
            }
        }

        public List<MarketGroup> MarketGroups
        {
            get
            {
                if(_marketGroups == null)
                    _marketGroups = Context.MarketGroups.ToList();
                return _marketGroups;
            }
        }

        public List<MarketGroup> MarketGroupTree
        {
            get
            {
                if (null == _marketGroupTree)
                {
                    // get root market groups
                    _marketGroupTree = MarketGroups.Where(x => x.ParentMarketGroupId == null).ToList();

                    // fill children
                    Queue<MarketGroup> groupsToProcess = new Queue<MarketGroup>(_marketGroupTree);
                    while (groupsToProcess.Count > 0)
                    {
                        var group = groupsToProcess.Dequeue();
                        group.Children = MarketGroups.Where(x => x.ParentMarketGroupId == group.Id).ToList();
                        foreach (var child in group.Children)
                        {
                            child.ParentMarketGroup = group;
                            groupsToProcess.Enqueue(child);
                        }
                    }
                }

                return _marketGroupTree;
            }
        }

        public Dictionary<int, EveType> Types
        {
            get
            {
                if(_types == null)
                {
                    _types = Context.Types.ToDictionary(x => x.Id);
                }

                return _types;
            }
        }


    }
}
