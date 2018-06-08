using System.Globalization;
using Newtonsoft.Json;


namespace EVEMarket.Model
{
    public class Text
    {
        [JsonProperty("de")]
        public string De { get; set; } = string.Empty;

        [JsonProperty("en")]
        public string En { get; set; } = string.Empty;

        [JsonProperty("fr")]
        public string Fr { get; set; } = string.Empty;

        [JsonProperty("ja")]
        public string Ja { get; set; } = string.Empty;

        [JsonProperty("es")]
        public string Es { get; set; } = string.Empty;

        [JsonProperty("it")]
        public string It { get; set; } = string.Empty;

        [JsonProperty("ru")]
        public string Ru { get; set; } = string.Empty;

        [JsonProperty("zh")]
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

                case "es":
                    return Es;

                case "it":
                    return It;

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