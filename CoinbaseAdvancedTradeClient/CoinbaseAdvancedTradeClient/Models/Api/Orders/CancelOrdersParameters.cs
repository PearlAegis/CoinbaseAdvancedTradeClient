using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class CancelOrdersParameters
    {
        [JsonProperty("order_ids")]
        public List<string> OrderIds { get; set; }
    }
}
