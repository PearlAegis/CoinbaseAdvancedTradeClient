using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api
{
    public class Candle
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("low")]
        public string Low { get; set; }

        [JsonProperty("high")]
        public string High { get; set; }

        [JsonProperty("open")]
        public string Open { get; set; }

        [JsonProperty("close")]
        public string Close { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }
    }
}
