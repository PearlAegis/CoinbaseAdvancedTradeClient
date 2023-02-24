using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface ITransactionSummaryEndpoint
    {
        //TODO Models
        Task<object> GetTransactionSummaryAsync(DateTime startDate, DateTime endDate, string userNativeCurrency, string productType);
    }
}
