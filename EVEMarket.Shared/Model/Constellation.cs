using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EVEMarket.Model
{
    [Table("mapConstellations")]
    public class Constellation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("constellationID")]
        public int Id { get; set; }

        [Column("regionID")]
        [ForeignKey("Region")]
        public int RegionId { get; set; }

        public Region Region { get; set; }
        
        [Column("constellationName")]
        public string Name { get; set; }

        [Column("radius")]
        public double? Radius { get; set; }

        [NotMapped]
        public int? WormholeClassId { get; set; }

        public int? FactionId { get; set; }
       
        public virtual ICollection<SolarSystem> Systems { get; set; } = new List<SolarSystem>();
    }
}