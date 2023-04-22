using CoinbaseAdvancedTradeClient.Models.WebSocket.Events;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket
{
    public class WebSocketMessage
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("sequence_num")]
        public string SequenceNumber { get; set; }

        [JsonProperty("events")]
        public List<WebSocketEvent> Events { get; set; }
    }
}
