using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api;
using CoinbaseAdvancedTradeClient.Models.Pages;
using Flurl;
using Flurl.Http;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IAccountsEndpoint
    {
        public IAccountsEndpoint Accounts => this;

        async Task<AccountsPage> IAccountsEndpoint.GetListAccountsAsync(int? limit = null, string cursor = null)
        {
            try
            {
                var response = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.AccountsEndpoint)
                    .SetQueryParam(nameof(limit), limit)
                    .SetQueryParam(nameof(cursor), cursor)
                    .GetJsonAsync<AccountsPage>();

                return response;
            }
            catch (Exception ex)
            {
                return new AccountsPage();
            }
        }

        async Task<Account> IAccountsEndpoint.GetAccountAsync(string accountId)
        {
            try
            {
                var response = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.AccountsEndpoint)
                    .AppendPathSegment(accountId)
                    .GetJsonAsync<AccountResponse>();

                return response.Account;
            }
            catch (Exception ex)
            {
                var type = ex.GetType().Name;
                return new Account();
            }
        }

    }
}
