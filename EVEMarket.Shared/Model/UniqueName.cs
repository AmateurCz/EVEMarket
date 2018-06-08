using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EVEMarket.Model
{
    [Table("invUniqueNames")]
    public class UniqueName
    {
        [Key]
        [Column("itemID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        
        [Column("itemName")]
        public string ItemName { get; set; }

        [Column("groupID")]
        public long GroupId { get; set; }
    }
}