using System.Collections.Generic;

namespace EVEMarket.Model
{
    public class Region
    {
        public int Id { get; set; }

        public int NameId { get; set; }

        public string Name { get; set; }

        public int? DescriptionId { get; set; }

        public int? FactionId { get; set; }

        public virtual ICollection<Constellation> Constellations { get; set; } = new List<Constellation>();
        public virtual ICollection<SolarSystem> SolarSystems { get; set; } = new List<SolarSystem>();
    }
}