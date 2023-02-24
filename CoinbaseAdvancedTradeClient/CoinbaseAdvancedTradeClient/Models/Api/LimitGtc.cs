using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api
{
    public class LimitGtc
    {
        [JsonProperty("base_size")]
        public string BaseSize { get; set; }

        [JsonProperty("limit_price")]
        public string LimitPrice { get; set; }

        [JsonProperty("post_only")]
        public bool PostOnly { get; set; }
    }
}
