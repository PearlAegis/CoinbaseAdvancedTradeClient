using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseAdvancedTradeClient.Interfaces
{
    public interface ICoinbaseAdvancedTradeApiClient
    {
        IAccountsEndpoint Accounts { get; }
        IOrdersEndpoint Orders { get; }
        IProductsEndpoint Products { get; }
        ITransactionSummaryEndpoint TransactionSummary { get; }
    }
}
