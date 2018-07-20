using System;

namespace EVEMarket.Model
{
    public class SolarSystem
    {
        public int Id { get; set; }

        public int ConstellationId { get; set; }

        public Constellation Constellation { get; set; }

        public String Name { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        public float Security { get; set; }

        public string SecurityClass { get; set; }
    }
}