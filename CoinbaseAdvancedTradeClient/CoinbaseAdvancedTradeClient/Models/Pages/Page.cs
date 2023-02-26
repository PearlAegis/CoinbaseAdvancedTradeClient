using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class Page
    {
        [JsonProperty("has_next")]
        public bool HasNext { get; set; }
        
        [JsonProperty("cursor")]
        public string Cursor { get; set; }
    }
}
