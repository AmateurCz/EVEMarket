using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMarket.Model
{
    public class Graphic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("Description")]
        public string description;

        [JsonProperty("iconFolder")]
        public string IconFolder;

        [JsonProperty("sofFactionName")]
        public string SofFactionName;

        [JsonProperty("sofHullName")]
        public string SofHullName;

        [JsonProperty("sofRaceName")]
        public string SofRaceName;
    }
}