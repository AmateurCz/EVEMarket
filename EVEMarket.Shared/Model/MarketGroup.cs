using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMarket.Model
{
    public class MarketGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("marketGroupName")]
        public string Name { get; set; }

        [JsonProperty("iconID")]
        public int IconId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("hasTypes")]
        public bool HasTypes { get; set; }

        [JsonProperty("parentGroupID")]
        [ForeignKey("ParentMarketGroup")]
        public int? ParentMarketGroupId { get; set; }

        [JsonIgnore]
        public virtual MarketGroup ParentMarketGroup { get; set; }     
    }
}
