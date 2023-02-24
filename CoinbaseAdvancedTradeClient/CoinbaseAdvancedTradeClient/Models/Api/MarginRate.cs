using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api
{
    public class MarginRate
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
