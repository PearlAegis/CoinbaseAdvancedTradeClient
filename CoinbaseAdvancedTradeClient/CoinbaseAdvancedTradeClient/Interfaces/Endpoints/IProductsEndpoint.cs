using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.Products;
using CoinbaseAdvancedTradeClient.Models.Pages;

namespace CoinbaseAdvancedTradeClient.Interfaces.Endpoints
{
    public interface IProductsEndpoint
    {
        Task<ApiResponse<ProductsPage>> GetListProductsAsync(int? limit, int? offset, string productType);
        Task<ApiResponse<Product>> GetProductAsync(string productId);
        Task<ApiResponse<CandlesPage>> GetProductCandlesAsync(string productId, DateTimeOffset start, DateTimeOffset end, string granularity);
        Task<ApiResponse<TradesPage>> GetMarketTradesAsync(string productId, int limit);
    }
}
