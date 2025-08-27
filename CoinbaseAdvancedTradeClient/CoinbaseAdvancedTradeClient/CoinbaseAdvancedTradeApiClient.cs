using CoinbaseAdvancedTradeClient.Authentication;
using CoinbaseAdvancedTradeClient.Interfaces;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Config;
using CoinbaseAdvancedTradeClient.Resources;
using Flurl.Http;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : FlurlClient, ICoinbaseAdvancedTradeApiClient
    {
        private readonly SecretApiKeyConfig _config;

        public CoinbaseAdvancedTradeApiClient(SecretApiKeyConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config), ErrorMessages.ApiConfigRequired);
            if (string.IsNullOrWhiteSpace(config.KeyName)) throw new ArgumentException(ErrorMessages.ApiKeyRequired, nameof(config.KeyName));
            if (string.IsNullOrWhiteSpace(config.KeySecret)) throw new ArgumentException(ErrorMessages.ApiSecretRequired, nameof(config.KeySecret));

            _config = config;

            this.BeforeCall(SecretApiKeyAuthentication);
        }

        #region Authentication

        private void SecretApiKeyAuthentication(FlurlCall call)
        {
            var method = call.Request.Verb.Method.ToUpperInvariant();
            var uri = call.Request.Url.ToUri();
            var requestHost = uri.Host;
            var requestPath = uri.PathAndQuery;

            var jwt = SecretApiKeyAuthenticator.GenerateBearerJWT(
                _config.KeyName, 
                _config.KeySecret, 
                method, 
                requestHost, 
                requestPath);

            call.Request.WithHeader("Authorization", $"Bearer {jwt}");
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
