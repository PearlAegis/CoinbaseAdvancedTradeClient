using CoinbaseAdvancedTradeClient.Constants;

namespace CoinbaseAdvancedTradeClient.Models.Config
{
    public class SecretApiKeyConfig
    {
        public string KeyName { get; set; }
        public string KeySecret { get; set; }
        public string ApiUrl { get; set; } = ApiEndpoints.ApiEndpointBase;
    }
}