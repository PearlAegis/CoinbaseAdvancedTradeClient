﻿using CoinbaseAdvancedTradeClient.Enums;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class Fill
    {
        [JsonProperty("entry_id")]
        public string EntryId { get; set; }

        [JsonProperty("trade_id")]
        public string TradeId { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("trade_time")]
        public DateTimeOffset? TradeTime { get; set; }

        [JsonProperty("trade_type")]
        public string TradeType { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonProperty("size")]
        public decimal? Size { get; set; }

        [JsonProperty("commission")]
        public decimal? Commission { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("sequence_timestamp")]
        public DateTimeOffset? SequenceTimestamp { get; set; }

        [JsonProperty("liquidity_indicator")]
        public string LiquidityIndicator { get; set; }

        [JsonProperty("size_in_quote")]
        public bool SizeInQuote { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("side")]
        public OrderSide Side { get; set; }
    }
}
