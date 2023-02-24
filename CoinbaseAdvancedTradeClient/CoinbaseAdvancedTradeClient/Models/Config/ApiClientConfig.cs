using CoinbaseAdvancedTradeClient.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseAdvancedTradeClient.Models.Config
{
    public class ApiClientConfig
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string ApiUrl { get; set; } = ApiEndpoints.ApiEndpointBase;
    }
}
