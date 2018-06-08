using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMarket.Model
{
    [Table("invMarketGroups")]
    public class MarketGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("marketGroupId")]
        public int Id { get; set; }

        [Column("marketGroupName")]
        public string Name { get; set; }

        [Column("iconID")]
        public int? IconId { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("hasTypes")]
        public bool HasTypes { get; set; }

        [ForeignKey("ParentMarketGroup")]
        [Column("parentGroupID")]
        public int? ParentMarketGroupId { get; set; }

        public virtual MarketGroup ParentMarketGroup { get; set; }

        public virtual List<MarketGroup> Children { get; set; } = new List<MarketGroup>();
    }
}