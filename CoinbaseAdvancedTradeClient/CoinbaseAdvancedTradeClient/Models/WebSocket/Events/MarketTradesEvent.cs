using CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events
{
    public class MarketTradesEvent : WebSocketEvent
    {
        [JsonProperty("trades")]
        public List<Trade> Trades { get; set; } = new List<Trade>();
    }
}
