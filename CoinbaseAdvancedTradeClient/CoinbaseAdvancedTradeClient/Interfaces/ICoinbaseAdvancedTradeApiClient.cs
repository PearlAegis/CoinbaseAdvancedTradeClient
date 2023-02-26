using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;

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
