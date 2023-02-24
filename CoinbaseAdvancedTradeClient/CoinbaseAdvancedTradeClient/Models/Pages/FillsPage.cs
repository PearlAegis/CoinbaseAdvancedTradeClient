using CoinbaseAdvancedTradeClient.Models.Api;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class FillsPage : Page
    {
        [JsonProperty("fills")]
        public List<Fill> Fills { get; set; } = new List<Fill>();
    }
}
