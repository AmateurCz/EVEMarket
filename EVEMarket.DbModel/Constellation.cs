using System.Collections.Generic;

namespace EVEMarket.Model
{
    public class Constellation
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        public string Name { get; set; }

        public double? Radius { get; set; }

        public int? FactionId { get; set; }

        public virtual ICollection<SolarSystem> SolarSystems { get; set; } = new List<SolarSystem>();
    }
}