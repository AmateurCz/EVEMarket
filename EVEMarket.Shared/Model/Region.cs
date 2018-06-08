using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EVEMarket.Model
{
    [Table("mapRegions")]
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("regionID")]
        public int Id { get; set; }

        [Column("nameID")]
        public int NameId { get; set; }

        [Column("regionName")]
        public string Name { get; set; }

        [Column("descriptionID")]
        public int? DescriptionId { get; set; }

        [Column("factionID")]
        public int? FactionId { get; set; }

        [NotMapped]
        public int? Nebula { get; set; }

        [NotMapped]
        public int? WormholeClassId { get; set; }

        [NotMapped]
        public Vector3 Center { get; set; }

        [NotMapped]
        public Vector3 Min { get; set; }

        [NotMapped]
        public Vector3 Max { get; set; }

        public virtual ICollection<Constellation> Constellations { get; set; } = new List<Constellation>();
    }
}