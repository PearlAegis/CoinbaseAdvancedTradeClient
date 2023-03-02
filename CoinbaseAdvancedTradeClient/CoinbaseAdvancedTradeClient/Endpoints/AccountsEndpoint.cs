using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api.Accounts;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Pages;
using CoinbaseAdvancedTradeClient.Resources;
using Flurl;
using Flurl.Http;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IAccountsEndpoint
    {
        public IAccountsEndpoint Accounts => this;

        async Task<ApiResponse<AccountsPage>> IAccountsEndpoint.GetListAccountsAsync(int? limit = null, string cursor = null)
        {
            var response = new ApiResponse<AccountsPage>();

            try
            {
                if (limit != null && (limit < 1 || limit > 250)) throw new ArgumentException(ErrorMessages.LimitParameterRange, nameof(limit));

                var accountsPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.AccountsEndpoint)
                    .SetQueryParam(RequestParameters.Limit, limit)
                    .SetQueryParam(RequestParameters.Cursor, cursor)
                    .GetJsonAsync<AccountsPage>();

                response.Data = accountsPage;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<Account>> IAccountsEndpoint.GetAccountAsync(string accountId)
        {
            var response = new ApiResponse<Account>();

            try
            {
                if (string.IsNullOrWhiteSpace(accountId)) throw new ArgumentNullException(nameof(accountId), ErrorMessages.AccountIdRequired);

                var accountsPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.AccountsEndpoint)
                    .AppendPathSegment(accountId)
                    .GetJsonAsync<AccountsPage>();

                response.Data = accountsPage.Account;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }
    }
}
