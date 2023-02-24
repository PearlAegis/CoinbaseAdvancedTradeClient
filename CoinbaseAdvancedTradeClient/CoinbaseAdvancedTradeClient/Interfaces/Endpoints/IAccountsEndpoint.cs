using CoinbaseAdvancedTradeClient.Models.Api;
using CoinbaseAdvancedTradeClient.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IAccountsEndpoint
    {
        Task<IList<Account>> GetListAccountsAsync();
        Task<Account> GetAccountAsync(string accountId);  
    }
}
