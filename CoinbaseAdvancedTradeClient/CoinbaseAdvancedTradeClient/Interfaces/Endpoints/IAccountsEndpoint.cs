using CoinbaseAdvancedTradeClient.Models.Api;
using CoinbaseAdvancedTradeClient.Models.Pages;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IAccountsEndpoint
    {
        Task<ApiResponse<AccountsPage>> GetListAccountsAsync(int? limit = null, string cursor = null);
        Task<ApiResponse<Account>> GetAccountAsync(string accountId);  
    }
}
