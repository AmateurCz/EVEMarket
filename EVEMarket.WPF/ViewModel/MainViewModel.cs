using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLocator;
using EVEMarket.Data.Providers;
using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore;

namespace EVEMarket.WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private RegionViewModel _selectedRegion;
        private ObservableCollection<RegionViewModel> _regions;
        private ObservableCollection<MarketGroupViewModel> _marketGroups;

        public RegionViewModel SelectedRegion
        {
            get => _selectedRegion;
            set => Set(ref _selectedRegion, value);
        }

        public ObservableCollection<RegionViewModel> Regions
        {
            get => _regions;
            set => Set(ref _regions, value);
        }

        public ObservableCollection<MarketGroupViewModel> MarketGroups
        {
            get => _marketGroups;
            set => Set(ref _marketGroups, value);
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _regions = new ObservableCollection<RegionViewModel>();
        }

        internal async Task Initialize()
        {
            if (!IsInDesignMode)
            {
                var staticData = ServiceLocator.Current.GetInstance<IStaticData>();
                var regions = await staticData.Regions.ToListAsync();
                var mGroups = await staticData.MarketGroups.Where(x => x.ParentMarketGroupId == null).ToListAsync();

                MarketGroups = new ObservableCollection<MarketGroupViewModel>(mGroups.Select(x => new MarketGroupViewModel(x)));
                Regions = new ObservableCollection<RegionViewModel>(regions.Select(x => new RegionViewModel(x)));
                SelectedRegion = Regions.First();
            }
        }
    }
}