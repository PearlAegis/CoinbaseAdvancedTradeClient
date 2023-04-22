using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items
{
    public class Update
    {
        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("event_time")]
        public string EventTime { get; set; }

        [JsonProperty("price_level")]
        public string PriceLevel { get; set; }

        [JsonProperty("new_quantity")]
        public string NewQuantity { get; set; }
    }
}
