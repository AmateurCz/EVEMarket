using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EVEMarket.Model
{
    public class MarketOrder
    {
        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("issued")]
        public DateTime Issued { get; set; }


        [JsonProperty("system_id")]
        public int SystemId { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }

        [JsonProperty("location_id")]
        public long LocationId { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("min_volume")]
        public int MinVolume { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("volume_remain")]
        public int VolumeRemain { get; set; }

        [JsonProperty("volume_total")]
        public int VolumeTotal { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }
}
