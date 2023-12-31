﻿using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.WebSocket.Events.Items
{
    public class Update
    {
        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("event_time")]
        public DateTimeOffset EventTime { get; set; }

        [JsonProperty("price_level")]
        public decimal? PriceLevel { get; set; }

        [JsonProperty("new_quantity")]
        public decimal? NewQuantity { get; set; }
    }
}
