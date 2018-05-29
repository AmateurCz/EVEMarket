using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    [Table("invTypes")]
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("typeID")]
        public int Id { get; set; }

        [YamlMember(Alias = "groupID")]
        //[ForeignKey("Group")]
        [Column("groupID")]
        public int? GroupId { get; set; }

        //public Group Group { get; set; }

        [YamlMember(Alias = "marketGroupID")]
        [ForeignKey("MarketGroup")]
        [Column("marketGroupID")]
        public int? MarketGroupId { get; set; }

        public MarketGroup MarketGroup { get; set; }

        [YamlMember(Alias = "masteries")]
        [NotMapped]
        public Dictionary<int, int[]> Masteries { get; set; }

        [YamlMember(Alias = "factionID")]
        [Column("factionID")]
        public int? FactionId { get; set; }

        [Column("typeName")]
        public string NameDb { get => Name.En; set => Name.En = value; }

        [Column("description")]
        public string DescriptionDb { get => Description.En; set => Description.En = value; }

        [YamlMember(Alias = "name")]
        [NotMapped]
        public Text Name { get; set; } = new Text();

        [YamlMember(Alias = "description")]
        [NotMapped]
        public Text Description { get; set; } = new Text();

        [YamlMember(Alias = "sofFactionName")]
        [Column("sofFactionName")]
        public string FactionName { get; set; }
        
        [YamlMember(Alias = "portionSize")]
        [Column("portionSize")]
        public int? PortionSize { get; set; }

        [YamlMember(Alias = "raceID")]
        [Column("raceID")]
        public int? RaceId { get; set; }

        [YamlMember(Alias = "basePrice")]
        [Column("basePrice")]
        public double? BasePrice { get; set; }

        [YamlMember(Alias = "capacity")]
        [Column("capacity")]
        public double? Capacity { get; set; }

        [YamlMember(Alias = "mass")]
        [Column("mass")]
        public double? Mass { get; set; }

        [YamlMember(Alias = "volume")]
        [Column("volume")]
        public double? Volume { get; set; }

        [YamlMember(Alias = "radius")]
        [Column("radius")]
        public double? Radius { get; set; }

        [YamlMember(Alias = "soundID")]
        [Column("soundID")]
        public int? SoundId { get; set; }

        [YamlMember(Alias = "iconID")]
        [ForeignKey("Icon")]
        [Column("iconID")]
        public int? IconId { get; set; }
        
        public Icon Icon { get; set; }

        [YamlMember(Alias = "graphicID")]
        [ForeignKey("graphic")]
        [Column("graphicID")]
        public int? GraphicId { get; set; }
        
        public Graphic Graphic { get; set; }

        [YamlMember(Alias = "published")]
        [Column("published")]
        public bool Published { get; set; }

        [YamlMember(Alias = "traits")]
        [NotMapped]
        public Traits Traits { get; set; }
        
        [YamlMember(Alias = "sofMaterialSetID")]
        [Column("sofMaterialSetID")]
        public int? SofMaterialSetId { get; set; }


        public override string ToString()
        {
            return $"{Id} - {Name.ToString()}";
        }
    }
}