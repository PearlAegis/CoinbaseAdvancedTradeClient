using CoinbaseAdvancedTradeClient.Constants;

namespace CoinbaseAdvancedTradeClient.Models.Config
{
    [Obsolete("ApiClientConfig is deprecated. Use SecretApiKeyConfig instead for the new Ed25519 JWT authentication.")]
    public class ApiClientConfig
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string ApiUrl { get; set; } = ApiEndpoints.ApiEndpointBase;
    }
}
