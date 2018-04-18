using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
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

        public ObservableCollection<RegionViewModel> Regions { get; }
        public RegionViewModel SelectedRegion
        {
            get => _selectedRegion;
            set => Set(ref _selectedRegion, value);
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                using (var stream = File.Open(@"C:\Users\kubatdav\Downloads\sde-20180323-TRANQUILITY.zip", FileMode.Open))
                {
                    var zipArchive = new ZipArchive(stream);

                    var regions = RegionBuilder.BuildRegionsFromZipFile(zipArchive)
                        .OrderBy(x => x.Id)
                        .Select(x => new RegionViewModel(x))
                        .ToList();

                    SelectedRegion = regions.First();
                    Regions = new ObservableCollection<RegionViewModel>(regions);



                }
            }
        }
    }
}