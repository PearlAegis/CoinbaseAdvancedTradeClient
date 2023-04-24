using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items
{
    public class Subscriptions
    {
        [JsonProperty("level2")]
        public List<string> Level2 { get; set; }

        [JsonProperty("market_trades")]
        public List<string> MarketTrades { get; set; }

        [JsonProperty("status")]
        public List<string> Status { get; set; }

        [JsonProperty("ticker")]
        public List<string> Ticker { get; set; }

        [JsonProperty("ticker_batch")]
        public List<string> TickerBatch { get; set; }

        [JsonProperty("user")]
        public List<string> User { get; set; }
    }
}
