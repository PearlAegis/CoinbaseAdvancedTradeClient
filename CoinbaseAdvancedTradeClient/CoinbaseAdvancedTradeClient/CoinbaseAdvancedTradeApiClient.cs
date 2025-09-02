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
        private CoinbaseClientConfig _config;

        public CoinbaseAdvancedTradeApiClient(CoinbaseClientConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config), ErrorMessages.ApiConfigRequired);
            if (string.IsNullOrWhiteSpace(config.KeyName)) throw new ArgumentException(ErrorMessages.ApiKeyRequired, nameof(config.KeyName));
            if (string.IsNullOrWhiteSpace(config.KeySecret)) throw new ArgumentException(ErrorMessages.ApiSecretRequired, nameof(config.KeySecret));

            _config = config;

            this.Configure(SecretApiKeyAuthentication);
        }

        #region Authentication

        private void SecretApiKeyAuthentication(ClientFlurlHttpSettings settings)
        {
            async Task SetHeaders(FlurlCall http)
            {
                var method = http.Request.Verb.Method.ToUpperInvariant();
                var url = http.Request.Url.ToUri().AbsolutePath;
                var host = http.Request.Url.ToUri().Host;
                var jwt = SecretApiKeyAuthenticator.GenerateBearerJWT(
                    _config.KeyName, 
                    _config.KeySecret, 
                    method, 
                    host, 
                    url);

                http.Request.WithHeader(RequestHeaders.Authorization, string.Format(ErrorMessages.BearerTokenFormat, jwt));
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
