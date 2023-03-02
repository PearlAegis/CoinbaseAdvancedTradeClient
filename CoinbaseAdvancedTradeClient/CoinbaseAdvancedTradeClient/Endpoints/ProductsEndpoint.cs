using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Pages;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IProductsEndpoint
    {
        public IProductsEndpoint Products => this;

        async Task<ApiResponse<ProductsPage>> IProductsEndpoint.GetListProducts(int limit, int offset, string productType)
        {
            var response = new ApiResponse<ProductsPage>();

            try
            {

            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<ProductsPage>> IProductsEndpoint.GetProduct(string productId)
        {
            var response = new ApiResponse<ProductsPage>();

            try
            {

            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<CandlesPage>> IProductsEndpoint.GetProductCandles(string productId, DateTime start, DateTime end, string granularity)
        {
            var response = new ApiResponse<CandlesPage>();

            try
            {

            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<TradesPage>> IProductsEndpoint.GetMarketTrades(string productId, int limit)
        {
            var response = new ApiResponse<TradesPage>();

            try
            {

            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }
    }
}
