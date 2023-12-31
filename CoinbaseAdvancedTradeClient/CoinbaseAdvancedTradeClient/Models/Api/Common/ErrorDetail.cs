﻿using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Common
{
    public class ErrorDetail
    {
        [JsonProperty("type_url")]
        public string TypeUrl { get; set; }

        [JsonProperty("value")]
        public byte Value { get; set; }
    }
}
