using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class Bonus
    {
        [YamlMember(Alias = "unitID")]
        public int UnitId { get; set; }

        [YamlMember(Alias = "importance")]
        public int Importance { get; set; }

        [YamlMember(Alias = "bonus")]
        public double BonusValue { get; set; }

        [YamlMember(Alias = "bonusText")]
        public Text Text { get; set; }

    }
}