using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    [Table("invMarketGroups")]
    public class MarketGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("marketGroupId")]
        [YamlMember(Alias = "marketGroupID")]
        public int Id { get; set; }

        [YamlMember(Alias = "marketGroupName")]
        [Column("marketGroupName")]
        public string Name { get; set; }

        [YamlMember(Alias = "iconID")]
        [Column("iconID")]
        public int? IconId { get; set; }

        [YamlMember(Alias = "description")]
        [Column("description")]
        public string Description { get; set; }

        [YamlMember(Alias = "hasTypes")]
        [Column("hasTypes")]
        public bool HasTypes { get; set; }

        [YamlMember(Alias = "parentGroupID")]
        [ForeignKey("ParentMarketGroup")]
        [Column("parentGroupID")]
        public int? ParentMarketGroupId { get; set; }

        public virtual MarketGroup ParentMarketGroup { get; set; }

        public virtual List<MarketGroup> Children { get; set; } = new List<MarketGroup>();
    }
}