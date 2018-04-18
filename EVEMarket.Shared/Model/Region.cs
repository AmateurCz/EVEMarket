using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [YamlMember(Alias = "regionID")]
        public int Id { get; set; }

        [YamlMember(Alias = "nameID")]
        public int NameId { get; set; }

        public string Name { get; set; }

        [YamlMember(Alias = "descriptionID")]
        public int DescriptionId { get; set; }

        [YamlMember(Alias = "factionID")]
        public int? FactionId { get; set; }

        [YamlMember(Alias = "nebula")]
        public int? Nebula { get; set; }

        [YamlMember(Alias = "wormholeClassID")]
        public int? WormholeClassId { get; set; }

        [YamlMember(Alias = "center")]
        public Vector3 Center { get; set; }

        [YamlMember(Alias = "min")]
        public Vector3 Min { get; set; }

        [YamlMember(Alias = "max")]
        public Vector3 Max { get; set; }

        [JsonIgnore]
        [InverseProperty("regionID")]
        public virtual ICollection<Constellation> Constellations { get; set; } = new List<Constellation>();

    }
}
