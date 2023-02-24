using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : ITransactionSummaryEndpoint
    {
        public ITransactionSummaryEndpoint TransactionSummary => this;

        public Task<object> GetTransactionSummaryAsync(DateTime startDate, DateTime endDate, string userNativeCurrency, string productType)
        {
            throw new NotImplementedException();
        }
    }
}
