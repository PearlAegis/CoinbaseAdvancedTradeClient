using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Accounts
{
    public class CurrencyValue
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("value")]
        public decimal? Value { get; set; }
    }
}
