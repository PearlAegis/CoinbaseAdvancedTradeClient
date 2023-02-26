using CoinbaseAdvancedTradeClient.Models.Api.Orders;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class FillsPage : Page
    {
        [JsonProperty("fills")]
        public List<Fill> Fills { get; set; } = new List<Fill>();
    }
}
