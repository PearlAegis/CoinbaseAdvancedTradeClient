using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface ITransactionSummaryEndpoint
    {
        Task<ApiResponse<TransactionSummary>> GetTransactionSummaryAsync(DateTime startDate, DateTime endDate, string userNativeCurrency, ProductType productType);
    }
}
