using CoinbaseAdvancedTradeClient.Models.Api.Orders;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class OrdersPage : Page
    {
        [JsonProperty("order")]
        public Order Order { get; set; }

        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }
    }
}
