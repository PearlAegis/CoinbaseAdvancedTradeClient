using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket
{
    public class Subscription
    {
        [JsonProperty("channels")]
        public JArray Channels { get; set; } = new JArray();

        [JsonProperty("product_ids")]
        public List<string> ProductIds { get; set; } = new List<string>();
    }
}
