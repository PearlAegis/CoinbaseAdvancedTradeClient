using CoinbaseAdvancedTradeClient.Enums;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class CreateOrderParameters
    {
        [JsonProperty("client_order_id")]
        public string ClientOrderId { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }
        
        [JsonProperty("side")]
        public OrderSide Side { get; set; }
        
        [JsonProperty("order_configuration")]
        public OrderConfiguration OrderConfiguration { get; set; }

    }
}
