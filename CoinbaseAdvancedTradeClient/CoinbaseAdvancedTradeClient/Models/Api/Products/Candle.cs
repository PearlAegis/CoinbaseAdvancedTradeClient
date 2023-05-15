using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Products
{
    public class Candle
    {
        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("low")]
        public decimal? Low { get; set; }

        [JsonProperty("high")]
        public decimal? High { get; set; }

        [JsonProperty("open")]
        public decimal? Open { get; set; }

        [JsonProperty("close")]
        public decimal? Close { get; set; }

        [JsonProperty("volume")]
        public decimal? Volume { get; set; }
    }
}
