using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class CreateOrderSuccessResponse
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("client_order_id")]
        public string ClientOrderId { get; set; }
    }
}
