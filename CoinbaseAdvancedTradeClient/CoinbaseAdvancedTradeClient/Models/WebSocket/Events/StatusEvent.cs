using CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events
{
    public class StatusEvent : WebSocketEvent
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set;} = new List<Product>();
    }
}
