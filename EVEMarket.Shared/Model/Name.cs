using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class Name
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [YamlMember(Alias = "itemID")]
        public int Id { get; set; }

        [YamlMember(Alias = "itemName")]
        public string ItemName { get; set; }
    }
}