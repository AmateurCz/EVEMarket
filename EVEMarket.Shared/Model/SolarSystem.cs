using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class SolarSystem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [YamlMember(Alias = "solarSystemID")]
        public int Id { get; set; }

        [ForeignKey("constellation")]
        public int ConstellationId { get; set; }

        public Constellation Constellation { get; set; }

        public String Name { get; set; }
    }
}
