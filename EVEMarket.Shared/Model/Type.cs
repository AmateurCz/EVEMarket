using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMarket.Model
{
    [Table("invTypes")]
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("typeID")]
        public int Id { get; set; }

        //[ForeignKey("Group")]
        [Column("groupID")]
        public int? GroupId { get; set; }

        [NotMapped]
        public Group Group { get; set; }

        [ForeignKey("MarketGroup")]
        [Column("marketGroupID")]
        public int? MarketGroupId { get; set; }

        public MarketGroup MarketGroup { get; set; }

        [NotMapped]
        public Dictionary<int, int[]> Masteries { get; set; }

        [Column("factionID")]
        public int? FactionId { get; set; }

        [Column("typeName")]
        public string NameDb { get => Name.En; set => Name.En = value; }

        [Column("description")]
        public string DescriptionDb { get => Description.En; set => Description.En = value; }

        [NotMapped]
        public Text Name { get; set; } = new Text();

        [NotMapped]
        public Text Description { get; set; } = new Text();

        [Column("sofFactionName")]
        public string FactionName { get; set; }

        [Column("portionSize")]
        public int? PortionSize { get; set; }

        [Column("raceID")]
        public int? RaceId { get; set; }

        [Column("basePrice")]
        public double? BasePrice { get; set; }

        [Column("capacity")]
        public double? Capacity { get; set; }

        [Column("mass")]
        public double? Mass { get; set; }

        [Column("volume")]
        public double? Volume { get; set; }

        [Column("radius")]
        public double? Radius { get; set; }

        [Column("soundID")]
        public int? SoundId { get; set; }

        [ForeignKey("Icon")]
        [Column("iconID")]
        public int? IconId { get; set; }

        public Icon Icon { get; set; }

        [ForeignKey("Graphic")]
        [Column("graphicID")]
        public int? GraphicId { get; set; }

        public Graphic Graphic { get; set; }

        [Column("published")]
        public bool Published { get; set; }

        [Column("sofMaterialSetID")]
        public int? SofMaterialSetId { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name.ToString()}";
        }
    }
}