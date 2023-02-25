using CoinbaseAdvancedTradeClient.Models.Api;
using CoinbaseAdvancedTradeClient.Models.Pages;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IAccountsEndpoint
    {
        Task<AccountsPage> GetListAccountsAsync(int? limit = null, string cursor = null);
        Task<Account> GetAccountAsync(string accountId);  
    }
}
