using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using CommonServiceLocator;
using GalaSoft.MvvmLight;

namespace EVEMarket.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase, Interfaces.VmWithInitialization
    {
        private PageViewModel _selectedPage;

        public ObservableCollection<PageViewModel> Pages { get; } = new ObservableCollection<PageViewModel>();

        public PageViewModel SelectedPage
        {
            get => _selectedPage;
            set => Set(ref _selectedPage, value);
        }

        public MainViewModel()
        {
        }

        public Task InitializeAsync()
        {
            Pages.Add(new PageViewModel
            {
                IconPath = "Icons/UI/WindowIcons/member.png",
                Name = "Account",
                Content = ServiceLocator.Current.GetInstance<AccountViewModel>()
            });

            Pages.Add(new PageViewModel
            {
                IconPath = "Icons/UI/WindowIcons/market.png",
                Name = "Market",
                Content = ServiceLocator.Current.GetInstance<MarketViewModel>()
            });

            Pages.Add(new PageViewModel
            {
                IconPath = "Icons/UI/WindowIcons/assets.png",
                Name = "Assets",
                Content = ServiceLocator.Current.GetInstance<AssetViewModel>()
            });

            SelectedPage = Pages.First();

            FindIconsForPages();
            return Task.CompletedTask;
        }

        private void FindIconsForPages()
        {
            using (var stream = File.OpenRead(Properties.Settings.Default.IconFile))
            {
                ZipArchive archive = new ZipArchive(stream);

                foreach (var page in Pages)
                {
                    var entry = archive.GetEntry(page.IconPath);
                    var bitmap = new BitmapImage();
                    using (var zipStream = entry.Open())
                    {
                        // If we dont copy content from zip stream to memory stream
                        // bitmap will be blank
                        using (var memoryStream = new MemoryStream())
                        {
                            zipStream.CopyTo(memoryStream);
                            memoryStream.Position = 0;
                            bitmap.BeginInit();
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.StreamSource = memoryStream;
                            bitmap.EndInit();
                        }
                    }

                    page.Icon = bitmap;
                }
            }
        }
    }
}