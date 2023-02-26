using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class MarketLoc
    {
        [JsonProperty("quote_size")]
        public string QuoteSize { get; set; }

        [JsonProperty("base_size")]
        public string BaseSize { get; set; }
    }
}
