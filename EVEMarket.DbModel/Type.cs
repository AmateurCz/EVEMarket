using System.Collections.Generic;

namespace EVEMarket.Model
{
    public class Type
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }

        public int? MarketGroupId { get; set; }

        public MarketGroup MarketGroup { get; set; }

        public Dictionary<int, int[]> Masteries { get; set; }

        public int? FactionId { get; set; }

        public string NameDb { get => Name.En; set => Name.En = value; }

        public string DescriptionDb { get => Description.En; set => Description.En = value; }

        public Text Name { get; set; } = new Text();

        public Text Description { get; set; } = new Text();

        public string FactionName { get; set; }

        public int? PortionSize { get; set; }

        public int? RaceId { get; set; }

        public double? BasePrice { get; set; }

        public double? Capacity { get; set; }

        public double? Mass { get; set; }

        public double? Volume { get; set; }

        public double? Radius { get; set; }

        public int? SoundId { get; set; }

        public int? IconId { get; set; }

        public Icon Icon { get; set; }

        public int? GraphicId { get; set; }

        public Graphic Graphic { get; set; }

        public bool Published { get; set; }

        public int? MaterialSetId { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name.ToString()}";
        }
    }
}