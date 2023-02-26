using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Products
{
    public class Trade
    {
        [JsonProperty("trade_id")]
        public string TradeId { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("time")]
        public DateTime? Time { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("bid")]
        public string Bid { get; set; }

        [JsonProperty("ask")]
        public string Ask { get; set; }
    }
}
