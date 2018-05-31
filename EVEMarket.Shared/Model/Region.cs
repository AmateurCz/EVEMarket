using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    [Table("mapRegions")]
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [YamlMember(Alias = "regionID")]
        [Column("regionID")]
        public int Id { get; set; }

        [Column("nameID")]
        [YamlMember(Alias = "nameID")]
        public int NameId { get; set; }

        [Column("regionName")]
        public string Name { get; set; }

        [Column("descriptionID")]
        [YamlMember(Alias = "descriptionID")]
        public int? DescriptionId { get; set; }

        [Column("factionID")]
        [YamlMember(Alias = "factionID")]
        public int? FactionId { get; set; }

        [NotMapped]
        [YamlMember(Alias = "nebula")]
        public int? Nebula { get; set; }

        [NotMapped]
        [YamlMember(Alias = "wormholeClassID")]
        public int? WormholeClassId { get; set; }

        [NotMapped]
        [YamlMember(Alias = "center")]
        public Vector3 Center { get; set; }

        [NotMapped]
        [YamlMember(Alias = "min")]
        public Vector3 Min { get; set; }

        [NotMapped]
        [YamlMember(Alias = "max")]
        public Vector3 Max { get; set; }

        public virtual ICollection<Constellation> Constellations { get; set; } = new List<Constellation>();
    }
}