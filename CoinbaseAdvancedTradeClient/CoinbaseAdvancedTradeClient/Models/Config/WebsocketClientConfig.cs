using CoinbaseAdvancedTradeClient.Constants;

namespace CoinbaseAdvancedTradeClient.Models.Config
{
    public class WebSocketClientConfig
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string WebSocketUrl { get; set; } = ApiEndpoints.WebSocketEndpoint;
    }
}
