using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMarket.Model
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("anchorable")]
        public bool Anchorable { get; set; }

        [JsonProperty("anchored")]
        public bool Anchored { get; set; }

        [ForeignKey("Category")]
        [JsonProperty("categoryID")]
        public int CategoryID { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }

        [JsonProperty("fittableNonSingleton")]
        public bool FittableNonSingleton { get; set; }

        [JsonProperty("name")]
        public Text Name { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }

        [JsonProperty("useBasePrice")]
        public bool UseBasePrice { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name.ToString()}";
        }
    }
}