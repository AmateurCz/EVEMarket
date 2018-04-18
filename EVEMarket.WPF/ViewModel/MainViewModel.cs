using EVEMarket.Model;
using EVEMarket.WPF.Data;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;

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
        public ObservableCollection<RegionViewModel> Regions { get; }

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
                using (var stream = File.Open(@"F:\EVESDE\sde-20180323-TRANQUILITY.zip", FileMode.Open))
                {
                    var zipArchive = new ZipArchive(stream);
                    Dictionary<int, string> names;
                    using (var namesYaml = zipArchive.Entries.FirstOrDefault(x => x.Name == "invNames.yaml").Open())
                       names = StaticDataSerializer.Deserialize<List<Name>>(namesYaml).ToDictionary(x=>x.Id, x=>x.ItemName);


                    var regions = RegionBuilder.BuildRegionsFromZipFile(zipArchive)
                    .Select(x => new RegionViewModel(x) { Name = names[x.NameId] } )
                    .ToList();                    

                    Regions = new ObservableCollection<RegionViewModel>(regions);
                }
            }
        }
    }
}