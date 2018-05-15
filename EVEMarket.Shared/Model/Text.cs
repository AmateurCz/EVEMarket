using Newtonsoft.Json;
using System.Globalization;
using YamlDotNet.Serialization;

namespace EVEMarket.Model
{
    public class Text
    {
        [JsonProperty("de")]
        [YamlMember(Alias = "de")]
        public string De { get; set; } = string.Empty;

        [JsonProperty("en")]
        [YamlMember(Alias = "en")]
        public string En { get; set; } = string.Empty;

        [JsonProperty("fr")]
        [YamlMember(Alias = "fr")]
        public string Fr { get; set; } = string.Empty;

        [JsonProperty("ja")]
        [YamlMember(Alias = "ja")]
        public string Ja { get; set; } = string.Empty;

        [JsonProperty("es")]
        [YamlMember(Alias = "es")]
        public string Es { get; set; } = string.Empty;

        [JsonProperty("it")]
        [YamlMember(Alias = "it")]
        public string It { get; set; } = string.Empty;

        [JsonProperty("ru")]
        [YamlMember(Alias = "ru")]
        public string Ru { get; set; } = string.Empty;

        [JsonProperty("zh")]
        [YamlMember(Alias = "zh")]
        public string Zh { get; set; } = string.Empty;

        public override string ToString()
        {            
            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower())
            {
                case "de":
                    return De;
                case "fr":
                    return Fr;
                case "ja":
                    return Ja;
                case "ru":
                    return Ru;
                case "zh":
                    return Zh;
                case "en":
                default:
                    return En;
            }
        }
    }
}