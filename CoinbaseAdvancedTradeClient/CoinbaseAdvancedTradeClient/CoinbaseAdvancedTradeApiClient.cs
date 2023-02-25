using CoinbaseAdvancedTradeClient.Authentication;
using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Resources;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : FlurlClient, ICoinbaseAdvancedTradeApiClient
    {
        public ApiClientConfig? Config { get; private set; }

        public CoinbaseAdvancedTradeApiClient(ApiClientConfig? config = null)
        {
            ValidateConfig(config);
            this.Configure(ApiKeyAuth);
        }

        private void ValidateConfig(ApiClientConfig? config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config), ErrorMessages.ApiConfigRequired);
            if (string.IsNullOrWhiteSpace(config.ApiKey)) throw new ArgumentException(ErrorMessages.ApiKeyRequired, nameof(config.ApiKey));
            if (string.IsNullOrWhiteSpace(config.ApiSecret)) throw new ArgumentException(ErrorMessages.ApiSecretRequired, nameof(config.ApiSecret));

            Config = config;
        }

        private void ApiKeyAuth(ClientFlurlHttpSettings settings)
        {
            async Task SetHeaders(FlurlCall http)
            {
                var body = http.RequestBody;
                var method = http.Request.Verb.Method.ToUpperInvariant();
                var url = http.Request.Url.ToUri().AbsolutePath;
                var timestamp = ApiKeyAuthenticator.GenerateTimestamp();
                var signature = ApiKeyAuthenticator.GenerateSignature(Config.ApiSecret, timestamp, method, url, body);

                http.Request
                   .WithHeader(RequestHeaders.AccessKey, Config.ApiKey)
                   .WithHeader(RequestHeaders.AccessSign, signature)
                   .WithHeader(RequestHeaders.AccessTimestamp, timestamp);
            }

            settings.BeforeCallAsync = SetHeaders;
        }
    }
}
