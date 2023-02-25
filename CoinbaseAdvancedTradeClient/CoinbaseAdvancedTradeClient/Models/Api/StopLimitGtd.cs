using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api
{
    public class StopLimitGtd : StopLimitGtc
    {
        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }
    }
}
