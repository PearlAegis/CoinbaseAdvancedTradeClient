using CoinbaseAdvancedTradeClient.Constants;

namespace CoinbaseAdvancedTradeClient.Models.Config
{
    public class CoinbaseClientConfig
    {
        public string KeyName { get; set; }
        public string KeySecret { get; set; }
        public string ApiBaseUrl { get; set; } = ApiEndpoints.ApiEndpointBase;
        public string WebSocketUrl { get; set; } = ApiEndpoints.WebSocketEndpoint;
    }
}
