using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class LimitGtc
    {
        [JsonProperty("base_size")]
        public decimal? BaseSize { get; set; }

        [JsonProperty("limit_price")]
        public decimal? LimitPrice { get; set; }

        [JsonProperty("post_only")]
        public bool PostOnly { get; set; }
    }
}
