using CoinbaseAdvancedTradeClient.Constants;

namespace CoinbaseAdvancedTradeClient.Models.Config
{
    [Obsolete("WebSocketClientConfig is deprecated. Use SecretApiKeyWebSocketConfig instead for the new Ed25519 JWT authentication.")]
    public class WebSocketClientConfig
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string WebSocketUrl { get; set; } = ApiEndpoints.WebSocketEndpoint;
    }
}
