using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EVEMarket.Model
{
    public class Graphic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("iconFolder")]
        public string IconFolder { get; set; }

        [JsonProperty("sofFactionName")]
        public string SofFactionName { get; set; }

        [JsonProperty("sofHullName")]
        public string SofHullName { get; set; }

        [JsonProperty("sofRaceName")]
        public string SofRaceName { get; set; }
    }
}