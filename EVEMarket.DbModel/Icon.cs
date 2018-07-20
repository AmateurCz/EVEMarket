using System.Collections.Generic;

namespace EVEMarket.Model
{
    public class Icon
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string File { get; set; }

        public virtual List<Type> AttachedTypes { get; set; } = new List<Type>();
    }
}