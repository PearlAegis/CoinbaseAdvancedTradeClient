using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Pages;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IProductsEndpoint
    {
        Task<ApiResponse<ProductsPage>> GetListProducts(int limit, int offset, string productType);
        Task<ApiResponse<ProductsPage>> GetProduct(string productId);
        Task<ApiResponse<CandlesPage>> GetProductCandles(string productId, DateTime start, DateTime end, string granularity);
        Task<ApiResponse<TradesPage>> GetMarketTrades(string productId, int limit);
    }
}
