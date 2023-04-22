using CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events
{
    public class UserEvent : WebSocketEvent
    {
        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }
    }
}
