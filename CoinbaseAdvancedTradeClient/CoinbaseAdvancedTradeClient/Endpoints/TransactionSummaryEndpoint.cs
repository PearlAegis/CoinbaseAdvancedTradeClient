using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : ITransactionSummaryEndpoint
    {
        public ITransactionSummaryEndpoint TransactionSummary => this;

        Task<object> ITransactionSummaryEndpoint.GetTransactionSummaryAsync(DateTime startDate, DateTime endDate, string userNativeCurrency, string productType)
        {
            throw new NotImplementedException();
        }
    }
}
