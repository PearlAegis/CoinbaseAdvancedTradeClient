using CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events
{
    public class SubscriptionEvent : WebSocketEvent
    {
        [JsonProperty("subscriptions")]
        public JObject Subscriptions { get; set; }
    }
}
