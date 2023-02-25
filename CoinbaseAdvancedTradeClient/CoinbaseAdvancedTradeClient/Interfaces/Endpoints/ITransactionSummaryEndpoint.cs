namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface ITransactionSummaryEndpoint
    {
        //TODO Models
        Task<object> GetTransactionSummaryAsync(DateTime startDate, DateTime endDate, string userNativeCurrency, string productType);
    }
}
