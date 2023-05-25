using CoinbaseAdvancedTradeClient.Enums;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.TransactionSummaries;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface ITransactionSummaryEndpoint
    {
        Task<ApiResponse<TransactionSummary>> GetTransactionSummaryAsync(DateTimeOffset startDate, DateTimeOffset endDate, string userNativeCurrency, ProductType productType);
    }
}
