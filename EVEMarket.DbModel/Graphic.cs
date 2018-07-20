using System.Collections.Generic;

namespace EVEMarket.Model
{
    public class Graphic
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string File { get; set; }

        public string IconFolder { get; set; }

        public string FactionName { get; set; }

        public string HullName { get; set; }

        public string RaceName { get; set; }

        public virtual List<Type> AttachedTypes { get; set; } = new List<Type>();
    }
}