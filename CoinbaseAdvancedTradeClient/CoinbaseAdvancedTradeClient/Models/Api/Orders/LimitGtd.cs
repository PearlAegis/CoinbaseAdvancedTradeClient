using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Orders
{
    public class LimitGtd : LimitGtc
    {
        [JsonProperty("end_time")]
        public DateTimeOffset? EndTime { get; set; }
    }
}
