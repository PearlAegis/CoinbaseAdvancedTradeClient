using CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events
{
    public class TickerEvent : WebSocketEvent
    {
        [JsonProperty("tickers")]
        public List<Ticker> Tickers { get; set; }
    }
}
