using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using EVEMarket.WPF.Data;
using GalaSoft.MvvmLight;

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
                List<Model.Type> types = null;
                List<RegionViewModel> regions = null;
                List<MarketGroupViewModel> marketGroups = null;
                
                await Task.Run(() =>
                {
                    using (var stream = File.Open(@"C:\Users\kubatdav\Downloads\sde-20180323-TRANQUILITY.zip", FileMode.Open))
                    {
                        var zipArchive = new ZipArchive(stream);

                        using (var typeStream = zipArchive.GetEntry("sde/fsd/typeIDs.yaml").Open())
                        {
                            var typeModel = StaticDataSerializer.Deserialize<Dictionary<int, Model.Type>>(typeStream);

                            types = typeModel.Where(x => x.Value.Published && x.Value.MarketGroupId != null).Select(x =>
                            {
                                var value = x.Value;
                                value.Id = x.Key;
                                return value;
                            }).ToList();
                        }

                        regions = RegionBuilder.BuildRegionsFromZipFile(zipArchive)
                            .OrderBy(x => x.Id)
                            .Select(x => new RegionViewModel(x))
                            .ToList();

                        using (var groupStream = zipArchive.GetEntry("sde/bsd/invMarketGroups.yaml").Open())
                        {
                            var mgModel = StaticDataSerializer.Deserialize<List<Model.MarketGroup>>(groupStream);
                            marketGroups = mgModel.Where(x => x.ParentMarketGroupId == null).Select(x => new MarketGroupViewModel(x, mgModel, types)).ToList();
                        }
                    }
                });

                SelectedRegion = regions.First();

                Regions = new ObservableCollection<RegionViewModel>(regions);
                MarketGroups = new ObservableCollection<MarketGroupViewModel>(marketGroups);
            }
        }
    }
}