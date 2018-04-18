using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class UniqueName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [YamlMember(Alias = "itemID")]
        public int Id { get; set; }

        [YamlMember(Alias = "itemName")]
        public string ItemName { get; set; }

        [YamlMember(Alias = "groupID")]
        public string GroupId { get; set; }
    }
}
