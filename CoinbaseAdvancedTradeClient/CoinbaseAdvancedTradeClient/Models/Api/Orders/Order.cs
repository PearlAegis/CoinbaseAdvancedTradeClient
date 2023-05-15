using CoinbaseAdvancedTradeClient.Enums;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class Order
    {
        [JsonProperty("order_id")]
        public string Id { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("order_configuration")]
        public OrderConfiguration OrderConfiguration { get; set; }

        [JsonProperty("side")]
        public OrderSide Side { get; set; }

        [JsonProperty("client_order_id")]
        public string ClientOrderId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time_in_force")]
        public TimeInForce TimeInForce { get; set; }

        [JsonProperty("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonProperty("completion_percentage")]
        public decimal? CompletionPercentage { get; set; }

        [JsonProperty("filled_size")]
        public decimal? FilledSize { get; set; }

        [JsonProperty("average_filled_price")]
        public decimal? AverageFilledPrice { get; set; }

        [JsonProperty("fee")]
        public decimal? Fee { get; set; }

        [JsonProperty("number_of_fills")]
        public decimal? NumberOfFills { get; set; }

        [JsonProperty("filled_value")]
        public decimal? FilledValue { get; set; }

        [JsonProperty("pending_cancel")]
        public bool PendingCancel { get; set; }

        [JsonProperty("size_in_quote")]
        public bool SizeInQuote { get; set; }

        [JsonProperty("total_fees")]
        public decimal? TotalFees { get; set; }

        [JsonProperty("size_inclusive_of_fees")]
        public bool SizeInclusiveOfFees { get; set; }

        [JsonProperty("total_value_after_fees")]
        public decimal? TotalValueAfterFees { get; set; }

        [JsonProperty("trigger_status")]
        public string TriggerStatus { get; set; }

        [JsonProperty("order_type")]
        public string OrderType { get; set; }

        [JsonProperty("reject_reason")]
        public string RejectReason { get; set; }

        [JsonProperty("settled")]
        public bool Settled { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("reject_message")]
        public string RejectMessage { get; set; }

        [JsonProperty("cancel_message")]
        public string CancelMessage { get; set; }

        [JsonProperty("order_placement_source")]
        public string OrderPlacementSource { get; set; }

        [JsonProperty("outstanding_hold_amount")]
        public string OutstandingHoldAmount { get; set; }
    }
}
