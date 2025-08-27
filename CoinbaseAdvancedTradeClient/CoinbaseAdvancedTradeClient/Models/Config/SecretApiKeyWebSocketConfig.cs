using CoinbaseAdvancedTradeClient.Constants;

namespace CoinbaseAdvancedTradeClient.Models.Config
{
    public class SecretApiKeyWebSocketConfig
    {
        public string KeyName { get; set; }
        public string KeySecret { get; set; }
        public string WebSocketUrl { get; set; } = ApiEndpoints.WebSocketEndpoint;
    }
}