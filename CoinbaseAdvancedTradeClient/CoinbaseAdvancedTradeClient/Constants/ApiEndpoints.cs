namespace CoinbaseAdvancedTradeClient.Constants
{
    public sealed class ApiEndpoints
    {
        public const string ApiEndpointBase = "https://api.coinbase.com/api/v3/brokerage";
        public const string AccountsEndpoint = "accounts";
        public const string OrdersEndpoint = "orders";
        public const string OrdersBatchCancelEndpoint = $"{OrdersEndpoint}/batch_cancel";
        public const string OrdersHistoricalEndpoint = $"{OrdersEndpoint}/historical";
        public const string OrdersHistoricalBatchEndpoint = $"{OrdersHistoricalEndpoint}/batch";
        public const string OrdersHistoricalFillsEndpoint = $"{OrdersHistoricalEndpoint}/fills";
        public const string ProductsEndpoint = "products";
        public const string CandlesEndpoint = "candles";
        public const string TickerEndpoint = "ticker";
        public const string TransactionSummaryEndpoint = "transaction_summary";
    }
}
