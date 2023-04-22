using CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events
{
    public class Level2Event : WebSocketEvent
    {
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("updates")]
        public List<Update> Updates { get; set; }
    }
}
