using CoinbaseAdvancedTradeClient.Models.Api;
using Newtonsoft.Json;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class AccountsPage : Page
    {
        [JsonProperty("accounts")]
        public List<Account> Accounts { get; set; } = new List<Account>();

        [JsonProperty("size")]
        public int Size { get; set; }
    }
}
