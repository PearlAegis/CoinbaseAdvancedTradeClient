using CoinbaseAdvancedTradeClient.Models.Api.Products;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class TradesPage
    {
        [JsonProperty("trades")]
        public List<Trade> Trades { get; set; }

        [JsonProperty("best_bid")]
        public string BestBid { get; set; }

        [JsonProperty("best_ask")]
        public string BestAsk { get; set; }
    }
}
