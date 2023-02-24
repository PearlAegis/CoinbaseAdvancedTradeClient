using CoinbaseAdvancedTradeClient.Models.Api;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class TradesPage : Page
    {
        [JsonProperty("trades")]
        public List<Trade> Trades { get; set; } = new List<Trade>();

        [JsonProperty("best_bid")]
        public string BestBid { get; set; }

        [JsonProperty("best_ask")]
        public string BestAsk { get; set; }
    }
}
