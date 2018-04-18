using EVEMarket.Model;
using System;
using System.Collections.Generic;
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
            var universeArchives = archive.Entries.Where(x => x.FullName.StartsWith("sde/fsd/universe/")).ToList();

            foreach (var regionFile in universeArchives.Where(x => string.Equals(x.Name, "region.staticdata", StringComparison.InvariantCultureIgnoreCase)))
            {
                Region region;
                using (var stream = regionFile.Open())
                {
                    region = StaticDataSerializer.Deserialize<Region>(stream);
                }

                var regionPrefix = System.IO.Path.GetDirectoryName(regionFile.FullName)
                    .Replace('\\', '/');
                var regionName = System.IO.Path.GetFileName(regionPrefix);
                region.Name = regionName;


                foreach (var constellationFile in universeArchives.Where(
                    x => x.FullName.StartsWith(regionPrefix, StringComparison.InvariantCulture) &&
                         string.Equals(x.Name,
                                       "constellation.staticdata",
                                       StringComparison.InvariantCultureIgnoreCase)))
                {
                    Constellation con;
                    using (var stream = regionFile.Open())
                    {
                        con = StaticDataSerializer.Deserialize<Constellation>(stream);
                    }

                    
                    con.RegionId = region.Id;
                    con.Region = region;
                    region.Constellations.Add(con);

                    var constellationPrefix = System.IO.Path.GetDirectoryName(constellationFile.FullName).Replace('\\', '/');
                    var constellationName = System.IO.Path.GetFileName(constellationPrefix);
                    con.Name = constellationName;

                    //foreach (var systemFile in universeArchives.Where(
                    //    x => x.FullName.StartsWith(constellationPrefix, StringComparison.InvariantCulture) &&
                    //         string.Equals(x.Name,
                    //                       "system.staticdata",
                    //                       StringComparison.InvariantCultureIgnoreCase)))
                }

                yield return region;
            }
        }
    }
}
