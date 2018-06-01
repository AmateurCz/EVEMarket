using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    [Table("mapSolarSystems")]
    public class SolarSystem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [YamlMember(Alias = "solarSystemID")]
        [Column("solarSystemID")]
        public int Id { get; set; }

        [ForeignKey("Constellation")]
        [Column("constellationID")]
        public int ConstellationId { get; set; }

        public Constellation Constellation { get; set; }

        [Column("solarSystemName")]
        public String Name { get; set; }

        [ForeignKey("Region")]
        [Column("regionID")]
        public int RegionId { get; set; }

        public Region Region { get; set; }

        [Column("security")]
        public float Security { get; set; }
    }
}