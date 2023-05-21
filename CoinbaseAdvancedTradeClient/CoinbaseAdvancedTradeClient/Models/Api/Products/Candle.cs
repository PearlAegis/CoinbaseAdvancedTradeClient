using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbaseAdvancedTradeClient.Models.Api.Products
{
    public class Candle
    {
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("start")]
        public DateTimeOffset Start { get; set; }

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
