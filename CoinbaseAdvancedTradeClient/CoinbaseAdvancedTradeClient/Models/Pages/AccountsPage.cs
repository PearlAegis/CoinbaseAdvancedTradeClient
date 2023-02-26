using CoinbaseAdvancedTradeClient.Models.Api.Accounts;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class AccountsPage : Page
    {
        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("accounts")]
        public List<Account> Accounts { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }
    }
}
