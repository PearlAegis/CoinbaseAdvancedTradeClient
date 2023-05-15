using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class MarketIoc
    {
        [JsonProperty("quote_size")]
        public decimal? QuoteSize { get; set; }

        [JsonProperty("base_size")]
        public decimal? BaseSize { get; set; }
    }
}
