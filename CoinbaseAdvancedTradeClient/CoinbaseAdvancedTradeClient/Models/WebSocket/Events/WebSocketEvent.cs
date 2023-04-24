using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events
{
    public class WebSocketEvent
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
