using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EVEMarket.Model;
using EVEMarket.WPF.Interfaces;
using GalaSoft.MvvmLight;

namespace EVEMarket.WPF.ViewModel
{
    public class MarketViewModel : ViewModelBase, VmWithInitialization
    {
        private RegionViewModel _selectedRegion;
        private ReadOnlyCollection<RegionViewModel> _regions;
        private ReadOnlyCollection<MarketGroupViewModel> _marketGroups;
        private string _itemFilter;

        private List<MarketGroup> marketGroupCache = null;
        private List<Model.Type> typeCache = null;

        public RegionViewModel SelectedRegion
        {
            get => _selectedRegion;
            set => Set(ref _selectedRegion, value);
        }

        public ReadOnlyCollection<RegionViewModel> Regions
        {
            get => _regions;
            set => Set(ref _regions, value);
        }

        public string ItemFilter
        {
            get => _itemFilter;
            set => Set(ref _itemFilter, value);
        }

        public ReadOnlyCollection<MarketGroupViewModel> MarketGroups
        {
            get => _marketGroups;
            set => Set(ref _marketGroups, value);
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MarketViewModel()
        {
            _regions = new ReadOnlyCollection<RegionViewModel>(new List<RegionViewModel>());
        }

        public async Task InitializeAsync()
        {
            if (!IsInDesignMode)
            {
                using (var staticData = new Data.EveDbContext())
                {
                    var mGroups = await staticData.MarketGroups.ToListAsync();

                    var mGroupIds = mGroups.Select(x => x.Id).ToList();
                    var types = await staticData.Types.Where(x =>
                                    x.Published &&
                                    x.MarketGroupId.HasValue &&
                                    mGroupIds.Contains(x.MarketGroupId.Value))
                                .ToListAsync();

                    var regions = await staticData.Regions.ToListAsync();
                    Regions = new ReadOnlyCollection<RegionViewModel>(regions.Select(x => new RegionViewModel(x)).ToList());

                    this.marketGroupCache = mGroups;
                    this.typeCache = types;
                }

                GenerateMarketTree();
                SelectedRegion = Regions.First();
            }
        }

        private void GenerateMarketTree()
        {
            if (marketGroupCache == null || typeCache == null)
                return;

            MarketGroups = new ReadOnlyCollection<MarketGroupViewModel>(
                marketGroupCache.Where(x => x.ParentMarketGroupId == null).Select(x => new MarketGroupViewModel(x)).ToList());
        }

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(ItemFilter))
                GenerateMarketTree();

            base.RaisePropertyChanged(propertyName);
        }
    }
}