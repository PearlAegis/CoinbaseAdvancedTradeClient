using CoinbaseAdvancedTradeClient.Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
