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

        public async Task<IList<Account>> GetListAccountsAsync()
        {
            var endpoint = Config.ApiUrl.AppendPathSegment(ApiEndpoints.AccountsEndpoint);

            var response = await endpoint.WithClient(this).GetJsonAsync<AccountsPage>();

            return response.Accounts;
        }

        public Task<Account> GetAccountAsync(string accountId)
        {
            throw new NotImplementedException();
        }

    }
}
