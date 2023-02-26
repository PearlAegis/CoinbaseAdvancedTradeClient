using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries
{
    public class MarginRate
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
