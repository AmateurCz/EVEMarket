using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using EVEMarket.Model;
using EVEMarket.WPF.Data;
using EveType = EVEMarket.Model.Type;

namespace EVEMarket.DataProviders
{
    public class StaticData : IDisposable
    {
        private const string MarketGroupFileName = "sde/bsd/invMarketGroups.yaml";
        private const string TypeFileName = "sde/fsd/typeIDs.yaml";

        private ZipArchive _zipArchive;

        private List<MarketGroup> _marketGroups;
        private List<MarketGroup> _marketGroupTree;
        private Dictionary<int, EveType> _types;

        public List<MarketGroup> MarketGroups
        {
            get
            {
                if (null == _marketGroups)
                {
                    _marketGroups = StaticDataSerializer.Deserialize<List<MarketGroup>>(
                        GetStaticFile(MarketGroupFileName));
                }

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
                        group.Children = groupsToProcess.Where(x => x.ParentMarketGroupId == group.Id).ToList();
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
                if (null == _types)
                {
                    _types = StaticDataSerializer.Deserialize<Dictionary<int, EveType>>(GetStaticFile(TypeFileName));
                    foreach (var itm in _types)
                    {
                        itm.Value.Id = itm.Key;
                    }
                }

                return _types;
            }
        }

        private Stream GetStaticFile(string marketGroupFileName)
        {
            if (null == _zipArchive)
            {
                var stream = File.Open(@"C:\Users\kubatdav\Downloads\sde-20180323-TRANQUILITY.zip", FileMode.Open);
                _zipArchive = new ZipArchive(stream);
            }

            return _zipArchive.GetEntry(marketGroupFileName).Open();
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _zipArchive.Dispose();
                }

                _zipArchive = null;
                _marketGroups = null;

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}