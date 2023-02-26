using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class StopLimitGtc
    {
        [JsonProperty("base_size")]
        public string BaseSize { get; set; }

        [JsonProperty("limit_price")]
        public string LimitPrice { get; set; }

        [JsonProperty("stop_price")]
        public string StopPrice { get; set; }

        [JsonProperty("stop_direction")]
        public string StopDirection { get; set; }
    }
}
