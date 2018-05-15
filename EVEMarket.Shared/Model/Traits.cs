using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class Traits
    {
        [YamlMember(Alias = "roleBonuses")]
        public virtual List<Bonus> RoleBonuses { get; set; } = new List<Bonus>();

        [YamlMember(Alias = "miscBonuses")]
        public virtual List<Bonus> MiscBonuses { get; set; } = new List<Bonus>();
        
        [YamlMember(Alias = "types")]
        public virtual Dictionary<int, List<Bonus>> Types { get; set; } = new Dictionary<int, List<Bonus>>();
    }
}