using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries
{
    public class GoodsAndServicesTax
    {
        [JsonProperty("rate")]
        public decimal? Rate { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
