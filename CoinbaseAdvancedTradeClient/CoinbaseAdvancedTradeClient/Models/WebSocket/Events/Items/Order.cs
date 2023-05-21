using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items
{
    public class Order
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("client_order_id")]
        public string ClientOrderId { get; set; }

        [JsonProperty("cumulative_quantity")]
        public string CumulativeQuantity { get; set; }

        [JsonProperty("leaves_quantity")]
        public string LeavesQuantity { get; set; }

        [JsonProperty("avg_price")]
        public string AveragePrice { get; set; }

        [JsonProperty("total_fees")]
        public string TotalFees { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("creation_time")]
        public DateTimeOffset CreationTime { get; set; }

        [JsonProperty("order_side")]
        public string OrderSide { get; set; }

        [JsonProperty("order_type")]
        public string OrderType { get; set; }
    }
}
