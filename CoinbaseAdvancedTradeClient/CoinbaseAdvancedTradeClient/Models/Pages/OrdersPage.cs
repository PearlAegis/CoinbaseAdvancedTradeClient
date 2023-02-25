using CoinbaseAdvancedTradeClient.Models.Api;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class OrdersPage : Page
    {
        [JsonProperty("orders")]
        public List<Order> Orders { get; set; } = new List<Order>();

        [JsonProperty("sequence")]
        public long Sequence { get; set; }
    }
}
