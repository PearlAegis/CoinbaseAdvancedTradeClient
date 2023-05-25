using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class CreateOrderResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        
        [JsonProperty("failure_reason")]
        public string FailureReason { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("success_response")]
        public CreateOrderSuccessResponse SuccessResponse { get; set; }

        [JsonProperty("error_response")]
        public CreateOrderErrorResponse ErrorResponse { get; set; }
    }
}
