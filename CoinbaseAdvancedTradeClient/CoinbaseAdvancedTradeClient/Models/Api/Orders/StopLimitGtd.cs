﻿using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class StopLimitGtd : StopLimitGtc
    {
        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }
    }
}
