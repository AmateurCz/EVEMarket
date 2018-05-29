using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    [Table("mapConstellations")]
    public class Constellation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("constellationID")]
        [YamlMember(Alias = "constellationID")]
        public int Id { get; set; }

        [Column("regionID")]
        [ForeignKey("Region")]
        [YamlMember(Alias = "regionID")]
        public int RegionId { get; set; }

        public Region Region { get; set; }

        [YamlMember(Alias = "nameID")]
        [NotMapped]
        public int NameId { get; set; }

        [Column("constellationName")]
        public string Name { get; set; }

        [YamlMember(Alias = "radius")]
        [Column("radius")]
        public double? Radius { get; set; }

        [YamlMember(Alias = "wormholeClassID")]
        [NotMapped]
        public int? WormholeClassId { get; set; }

        [YamlMember(Alias = "factionID")]
        public int? FactionId { get; set; }

        [YamlMember(Alias = "center")]
        [NotMapped]
        public Vector3 Center { get; set; }

        [YamlMember(Alias = "min")]
        [NotMapped]
        public Vector3 Min { get; set; }

        [YamlMember(Alias = "max")]
        [NotMapped]
        public Vector3 Max { get; set; }

        public virtual ICollection<SolarSystem> Systems { get; set; } = new List<SolarSystem>();
    }
}