using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket
{
    public class Subscription
    {
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("channel")]
        public string Channel { get; set; } = string.Empty;

        [JsonProperty("product_ids")]
        public List<string> ProductIds { get; set; } = new List<string>();

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
