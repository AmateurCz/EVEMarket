using EVEMarket.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEMarket.WPF.Data
{
    public static class RegionBuilder
    {
        public static IEnumerable<Region> BuildRegionsFromZipFile(ZipArchive archive)
        {
            var fileNames = archive.Entries.Select(x => x.FullName.Replace('/', Path.DirectorySeparatorChar)).ToList();
            Func<string, Stream> openFile = path =>
            {
                int index = fileNames.IndexOf(path);
                return archive.Entries[index].Open();
            };

            return BuildRegions(fileNames, openFile);
        }

        public static IEnumerable<Region> BuildRegions(IEnumerable<string> fileNames, Func<string, Stream> readFile)
        {
            var universeArchives = fileNames.Where(x => x.StartsWith(Path.Combine("sde", "fsd", "universe"))).ToList();

            foreach (var regionFile in FindFiles("region.staticdata", universeArchives))
            {
                Region region;
                using (var stream = readFile(regionFile))
                {
                    region = StaticDataSerializer.Deserialize<Region>(stream);
                }

                var regionPrefix = Path.GetDirectoryName(regionFile);
                var regionName = Path.GetFileName(regionPrefix);
                region.Name = FormatName(regionName);


                foreach (var constellationFile in FindFiles(regionPrefix, "constellation.staticdata", universeArchives))
                {
                    Constellation con;
                    using (var stream = readFile(constellationFile))
                    {
                        con = StaticDataSerializer.Deserialize<Constellation>(stream);
                    }


                    con.RegionId = region.Id;
                    con.Region = region;
                    region.Constellations.Add(con);

                    var constellationPrefix = Path.GetDirectoryName(constellationFile);
                    var constellationName = Path.GetFileName(constellationPrefix);
                    con.Name = FormatName(constellationName);

                    foreach (var systemFile in FindFiles(constellationPrefix, "solarsystem.staticdata", universeArchives))
                    {
                        var systemPrefix = Path.GetDirectoryName(systemFile);
                        var systemName = Path.GetFileName(systemPrefix);

                        SolarSystem system = new SolarSystem();
                        system.Constellation = con;
                        system.ConstellationId = con.Id;
                        system.Name = FormatName(systemName);
                        con.Systems.Add(system);
                    }

                }

                yield return region;
            }
        }

        private static string FormatName(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            StringBuilder newText = new StringBuilder(text.Length + 5);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        private static IEnumerable<string> FindFiles(string fileName, IEnumerable<string> files)
        {
            return files.Where(x => x.EndsWith(fileName, StringComparison.InvariantCultureIgnoreCase));
        }
        private static IEnumerable<string> FindFiles(string prefix, string fileName, IEnumerable<string> files)
        {
            return files.Where(x => x.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase) && x.EndsWith(fileName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
