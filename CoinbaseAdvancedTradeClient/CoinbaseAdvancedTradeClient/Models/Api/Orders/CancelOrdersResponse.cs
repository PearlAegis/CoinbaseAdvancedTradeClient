using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class CancelOrdersResponse
    {
        [JsonProperty("results")]
        public List<CancelOrdersResult> Results { get; set; }
    }
}
