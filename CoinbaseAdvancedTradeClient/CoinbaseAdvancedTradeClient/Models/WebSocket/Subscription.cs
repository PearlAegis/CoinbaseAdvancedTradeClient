using CoinbaseAdvancedTradeClient.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket
{
    public class Subscription
    {
        [JsonProperty("type")]
        public SubscriptionTypes Type { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("product_ids")]
        public List<string> ProductIds { get; set; }

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
