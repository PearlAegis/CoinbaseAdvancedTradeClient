using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class OrderConfiguration
    {
        [JsonProperty("market_market_loc")]
        public MarketLoc MarketLoc { get; set; }

        [JsonProperty("limit_limit_gtc")]
        public LimitGtc LimitGtc { get; set; }

        [JsonProperty("limit_limit_gtd")]
        public LimitGtd LimitGtd { get; set; }

        [JsonProperty("stop_limit_stop_limit_gtc")]
        public StopLimitGtc StopLimitGtc { get; set; }

        [JsonProperty("stop_limit_stop_limit_gtd")]
        public StopLimitGtd StopLimitGtd { get; set; }
    }
}
