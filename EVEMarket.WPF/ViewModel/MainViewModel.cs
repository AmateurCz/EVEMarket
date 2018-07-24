using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using CommonServiceLocator;
using EVEMarket.WPF.Interfaces;
using EVEMarket.WPF.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace EVEMarket.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase, Interfaces.VmWithInitialization
    {
        protected NavigationHandler Navigator => ServiceLocator.Current.GetInstance<NavigationHandler>();

        public ObservableCollection<PageViewModel> Pages { get; } = new ObservableCollection<PageViewModel>();

        public MainViewModel()
        {
        }

        public Task InitializeAsync()
        {
            Pages.Add(new PageViewModel
            {
                IconPath = "Icons/UI/WindowIcons/member.png",
                Name = "User",
                Command = new RelayCommand(() => Navigator.NavigateTo(typeof(AccountDetails)))
            });

            Pages.Add(new PageViewModel
            {
                IconPath = "Icons/UI/WindowIcons/market.png",
                Name = "Market",
                Command = new RelayCommand(() => Navigator.NavigateTo(typeof(MarketDetails)))
            });

            Pages.Add(new PageViewModel
            {
                IconPath = "Icons/UI/WindowIcons/assets.png",
                Name = "Assets",
                Command = new RelayCommand(() => Navigator.NavigateTo(typeof(Assets)))
            });

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