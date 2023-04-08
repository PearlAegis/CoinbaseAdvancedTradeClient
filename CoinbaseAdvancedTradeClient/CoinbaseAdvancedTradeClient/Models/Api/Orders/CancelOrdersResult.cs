using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class CancelOrdersResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("failure_reason")]
        public string FailureReason { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }
    }
}
