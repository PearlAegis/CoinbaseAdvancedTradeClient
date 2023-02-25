namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IProductsEndpoint
    {
        //TODO Models
        Task<IList<object>> GetListProducts(object filterParameters);
        Task<object> GetProduct(string productId);
        Task<IList<object>> GetProductCandles(string productId, object filterParameters);
        Task<IList<object>> GetMarketTrades(string productId, int limit);
    }
}
