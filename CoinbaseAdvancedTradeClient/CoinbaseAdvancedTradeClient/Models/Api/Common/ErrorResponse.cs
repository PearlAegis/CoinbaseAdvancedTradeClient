using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Api.Common
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("details")]
        public List<ErrorDetail> Details { get; set; }
    }
}
