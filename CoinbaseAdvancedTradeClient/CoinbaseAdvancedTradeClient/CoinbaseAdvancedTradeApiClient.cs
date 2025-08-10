using CoinbaseAdvancedTradeClient.Authentication;
using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Resources;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : FlurlClient, ICoinbaseAdvancedTradeApiClient
    {
        private ApiClientConfig _config;

        public CoinbaseAdvancedTradeApiClient(ApiClientConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config), ErrorMessages.ApiConfigRequired);
            if (string.IsNullOrWhiteSpace(config.ApiKey)) throw new ArgumentException(ErrorMessages.ApiKeyRequired, nameof(config.ApiKey));
            if (string.IsNullOrWhiteSpace(config.ApiSecret)) throw new ArgumentException(ErrorMessages.ApiSecretRequired, nameof(config.ApiSecret));

            _config = config;

            this.Configure(ApiKeyAuthentication);
        }

        #region Authentication

        private void ApiKeyAuthentication(ClientFlurlHttpSettings settings)
        {
            async Task SetHeaders(FlurlCall http)
            {
                var body = http.RequestBody;
                var method = http.Request.Verb.Method.ToUpperInvariant();
                var url = http.Request.Url.ToUri().AbsolutePath;
                var timestamp = ApiKeyAuthenticator.GenerateTimestamp();
                var signature = ApiKeyAuthenticator.GenerateApiSignature(_config.ApiSecret, timestamp, method, url, body);

                http.Request
                   .WithHeader(RequestHeaders.AccessKey, _config.ApiKey)
                   .WithHeader(RequestHeaders.AccessSign, signature)
                   .WithHeader(RequestHeaders.AccessTimestamp, timestamp);
            }

            settings.BeforeCallAsync = SetHeaders;
        }

        #endregion // Authentication

        #region Exception Response Handling

        private async Task HandleExceptionResponseAsync<T>(Exception ex, ApiResponse<T> response)
        {
            response.Success = false;
            response.ExceptionType = ex.GetType().Name;
            response.ExceptionMessage = ex.Message;
            response.ExceptionDetails = await GetExceptionDetailsAsync(ex);
        }

        private async Task<string> GetExceptionDetailsAsync(Exception ex)
        {
            var flurlHttpException = (ex as FlurlHttpException);

            if (flurlHttpException != null)
            {
                try
                {
                    var error = await flurlHttpException.GetResponseJsonAsync<ErrorResponse>().ConfigureAwait(false);

                    return error?.Message ?? string.Empty;
                }
                catch
                {
                    return string.Empty;
                }

            }
            else
            {
                return string.Empty;
            }
        }

        #endregion // Exception Response Handling
    }
}
