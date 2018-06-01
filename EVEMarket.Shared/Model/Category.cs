using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EVEMarket.Model
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public Text Name { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name.ToString()}";
        }
    }
}