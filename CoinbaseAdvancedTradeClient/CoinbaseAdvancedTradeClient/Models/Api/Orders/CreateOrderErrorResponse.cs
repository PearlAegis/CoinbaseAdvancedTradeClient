using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class CreateOrderErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("error_details")]
        public string ErrorDetails { get; set; }

        [JsonProperty("preview_failure_reason")]
        public string PreviewFailureReason { get; set; }

        [JsonProperty("new_order_failure_reason")]
        public string NewOrderFailureReason { get; set; }

    }
}
