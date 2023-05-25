using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries
{
    public class MarginRate
    {
        [JsonProperty("value")]
        public decimal? Value { get; set; }
    }
}
