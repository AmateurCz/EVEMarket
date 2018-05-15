using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [YamlMember(Alias = "groupID")]
        [ForeignKey("Group")]
        public int? GroupId { get; set; }

        [YamlMember(Alias = "marketGroupID")]
        public int? MarketGroupId { get; set; }

        public Group Group { get; set; }

        [YamlMember(Alias = "masteries")]
        public Dictionary<int, int[]> Masteries { get; set; }

        [YamlMember(Alias = "factionID")]
        public int? FactionId { get; set; }
        
        [YamlMember(Alias = "name")]
        public Text Name { get; set; }

        [YamlMember(Alias = "description")]
        public Text Description { get; set; }

        [YamlMember(Alias = "sofFactionName")]
        public string FactionName { get; set; }
        
        [YamlMember(Alias = "portionSize")]
        public int? PortionSize { get; set; }

        [YamlMember(Alias = "raceID")]
        public int? RaceId { get; set; }

        [YamlMember(Alias = "basePrice")]
        public double? BasePrice { get; set; }

        [YamlMember(Alias = "capacity")]
        public double? Capacity { get; set; }

        [YamlMember(Alias = "mass")]
        public double? Mass { get; set; }

        [YamlMember(Alias = "volume")]
        public double? Volume { get; set; }

        [YamlMember(Alias = "radius")]
        public double? Radius { get; set; }

        [YamlMember(Alias = "soundID")]
        public int? SoundId { get; set; }

        [YamlMember(Alias = "iconID")]
        [ForeignKey("Icon")]
        public int? IconId { get; set; }
        
        public Icon Icon { get; set; }

        [YamlMember(Alias = "graphicID")]
        [ForeignKey("graphic")]
        public int? GraphicId { get; set; }
        
        public Graphic Graphic { get; set; }

        [YamlMember(Alias = "published")]
        public bool Published { get; set; }

        [YamlMember(Alias = "traits")]
        public Traits Traits { get; set; }
        
        [YamlMember(Alias = "sofMaterialSetID")]
        public int? SofMaterialSetId { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name.ToString()}";
        }
    }
}