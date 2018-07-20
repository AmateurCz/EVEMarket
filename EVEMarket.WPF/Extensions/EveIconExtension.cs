using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace EVEMarket.WPF.Extensions
{
    internal class EveIconExtension : MarkupExtension
    {
        private readonly string _path;

        public EveIconExtension(string path)
        {
            _path = path;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var bitmap = new BitmapImage();
            using (var stream = File.OpenRead(@"C:\Users\kubatdav\Downloads\SDE\July_Release_2018_Icons.zip"))
            {
                ZipArchive archive = new ZipArchive(stream);
                var entry = archive.GetEntry(_path);

                using (var zipStream = entry.Open())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        zipStream.CopyTo(memoryStream); // here
                        memoryStream.Position = 0;
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = memoryStream;
                        bitmap.EndInit();
                    }
                }

                return bitmap;
            }
        }
    }
}