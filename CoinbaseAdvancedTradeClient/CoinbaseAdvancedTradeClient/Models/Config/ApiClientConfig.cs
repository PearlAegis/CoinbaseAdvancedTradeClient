using CoinbaseAdvancedTradeClient.Constants;

namespace CoinbaseAdvancedTradeClient.Models.Config
{
    public class ApiClientConfig
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string ApiUrl { get; set; } = ApiEndpoints.ApiEndpointBase;
    }
}
