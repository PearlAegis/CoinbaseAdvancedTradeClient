using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket
{
    public class Channel
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("product_ids")]
        public List<string> ProductIds { get; set; }
    }
}
