using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class MarketGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [YamlMember(Alias = "marketGroupID")]
        public int Id { get; set; }

        [YamlMember(Alias = "marketGroupName")]
        public string Name { get; set; }

        [YamlMember(Alias = "iconID")]
        public int IconId { get; set; }

        [YamlMember(Alias = "description")]
        public string Description { get; set; }

        [YamlMember(Alias = "hasTypes")]
        public bool HasTypes { get; set; }

        [YamlMember(Alias = "parentGroupID")]
        [ForeignKey("ParentMarketGroup")]
        public int? ParentMarketGroupId { get; set; }

        [JsonIgnore]
        public virtual MarketGroup ParentMarketGroup { get; set; }     
    }
}
