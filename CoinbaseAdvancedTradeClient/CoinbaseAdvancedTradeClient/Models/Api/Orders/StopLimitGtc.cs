using CoinbaseAdvancedTradeClient.Enums;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class StopLimitGtc
    {
        [JsonProperty("base_size")]
        public decimal? BaseSize { get; set; }

        [JsonProperty("limit_price")]
        public decimal? LimitPrice { get; set; }

        [JsonProperty("stop_price")]
        public decimal? StopPrice { get; set; }

        [JsonProperty("stop_direction")]
        public StopDirection StopDirection { get; set; }
    }
}
