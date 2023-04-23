using CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events
{
    public class SubscriptionEvent : WebSocketEvent
    {
        [JsonProperty("subscriptions")]
        public Subscriptions Subscriptions { get; set; }
    }
}
