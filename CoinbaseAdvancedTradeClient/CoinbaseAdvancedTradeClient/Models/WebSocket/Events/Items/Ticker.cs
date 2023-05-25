using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items
{
    public class Ticker
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonProperty("volume_24_h")]
        public decimal? Volume24H { get; set; }

        [JsonProperty("low_24_h")]
        public decimal? Low24H { get; set; }

        [JsonProperty("high_24_h")]
        public decimal? High24H { get; set; }

        [JsonProperty("low_52_w")]
        public decimal? Low52W { get; set; }

        [JsonProperty("high_52_w")]
        public decimal? High52W { get; set; }

        [JsonProperty("price_percent_chg_24_h")]
        public decimal? PricePercentageChange24H { get; set; }
    }
}
