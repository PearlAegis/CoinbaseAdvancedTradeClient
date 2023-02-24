using CoinbaseAdvancedTradeClient.Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseAdvancedTradeClient.Models.Pages
{
    public class OrdersPage : Page
    {
        [JsonProperty("orders")]
        public List<Order> Orders { get; set; } = new List<Order>();

        [JsonProperty("sequence")]
        public long Sequence { get; set; }
    }
}
