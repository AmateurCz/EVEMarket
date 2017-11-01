using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMarket.Model
{
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("groupID")]
        [ForeignKey("Group")]
        public int GroupId { get; set; }

        [JsonIgnore]
        public Group Group { get; set; }

        [JsonProperty("name")]
        public Text Name { get; set; }

        [JsonProperty("description")]
        public Text Description { get; set; }

        [JsonProperty("portionSize")]
        public int PortionSize { get; set; }

        [JsonProperty("mass")]
        public double Mass { get; set; }

        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("radius")]
        public double Radius { get; set; }

        [JsonProperty("soundID")]
        public int SoundId { get; set; }

        [JsonProperty("iconID")]
        [ForeignKey("Icon")]
        public int IconId { get; set; }

        [JsonIgnore]
        public Icon Icon { get; set; }

        [JsonProperty("graphicID")]
        [ForeignKey("graphic")]
        public int GraphicId { get; set; }

        [JsonIgnore]
        public Graphic Graphic { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name.ToString()}";
        }
    }
}