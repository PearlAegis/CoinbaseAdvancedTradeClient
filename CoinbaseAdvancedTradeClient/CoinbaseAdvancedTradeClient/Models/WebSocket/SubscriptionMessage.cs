using CoinbaseAdvancedTradeClient.Enums;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket
{
    public class SubscriptionMessage
    {
        [JsonProperty("type")]
        public SubscriptionType Type { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("product_ids")]
        public List<string> ProductIds { get; set; }

        [JsonProperty("jwt")]
        public string Jwt { get; set; }
    }
}
