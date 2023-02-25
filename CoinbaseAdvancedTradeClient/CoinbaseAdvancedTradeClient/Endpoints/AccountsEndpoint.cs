using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api;
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
            if (limit != null && (limit < 1 || limit > 250)) throw new ArgumentException(nameof(limit), ErrorMessages.GetListAccountsLimitRange);

            var response = new ApiResponse<AccountsPage>();

            try
            {
                var accountsPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.AccountsEndpoint)
                    .SetQueryParam(nameof(limit), limit)
                    .SetQueryParam(nameof(cursor), cursor)
                    .GetJsonAsync<AccountsPage>();

                response.Data = accountsPage;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponse(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<Account>> IAccountsEndpoint.GetAccountAsync(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId)) throw new ArgumentNullException(nameof(accountId), ErrorMessages.GetAccountIdRequired);

            var response = new ApiResponse<Account>();

            try
            {
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
                await HandleExceptionResponse(ex, response);
            }

            return response;
        }

    }
}
