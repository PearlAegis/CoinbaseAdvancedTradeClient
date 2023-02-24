using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api
{
    public class LimitGtd : LimitGtc
    {
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }
    }
}
