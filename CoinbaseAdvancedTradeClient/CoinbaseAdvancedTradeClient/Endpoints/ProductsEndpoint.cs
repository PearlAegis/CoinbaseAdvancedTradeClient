using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IProductsEndpoint
    {
        public IProductsEndpoint Products => this;

        Task<IList<object>> IProductsEndpoint.GetListProducts(object filterParameters)
        {
            throw new NotImplementedException();
        }

        Task<IList<object>> IProductsEndpoint.GetMarketTrades(string productId, int limit)
        {
            throw new NotImplementedException();
        }

        Task<object> IProductsEndpoint.GetProduct(string productId)
        {
            throw new NotImplementedException();
        }

        Task<IList<object>> IProductsEndpoint.GetProductCandles(string productId, object filterParameters)
        {
            throw new NotImplementedException();
        }
    }
}
