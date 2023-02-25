using CoinbaseAdvancedTradeClient.Models.Api;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class AccountResponse
    {
        [JsonProperty("account")]
        public Account Account { get; set; }
    }
}
