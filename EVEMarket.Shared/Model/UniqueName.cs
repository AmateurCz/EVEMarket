using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    [Table("invUniqueNames")]
    public class UniqueName
    {
        [Key]
        [Column("itemID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [YamlMember(Alias = "itemID")]
        public long Id { get; set; }

        [YamlMember(Alias = "itemName")]
        [Column("itemName")]
        public string ItemName { get; set; }

        [YamlMember(Alias = "groupID")]
        [Column("groupID")]
        public string GroupId { get; set; }
    }
}