using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.Products;
using CoinbaseAdvancedTradeClient.Models.Pages;
using CoinbaseAdvancedTradeClient.Resources;
using Flurl.Http;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IProductsEndpoint
    {
        public IProductsEndpoint Products => this;

        async Task<ApiResponse<ProductsPage>> IProductsEndpoint.GetListProductsAsync(int limit, int offset, string productType)
        {
            var response = new ApiResponse<ProductsPage>();

            try
            {
                var productsPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.ProductsEndpoint)
                    .SetQueryParam(RequestParameters.Limit, limit)
                    .SetQueryParam(RequestParameters.Offset, offset)
                    .SetQueryParam(RequestParameters.ProductType, productType)
                    .GetJsonAsync<ProductsPage>();

                response.Data = productsPage;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<Product>> IProductsEndpoint.GetProductAsync(string productId)
        {
            var response = new ApiResponse<Product>();

            try
            {
                if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentNullException(nameof(productId), ErrorMessages.ProductIdRequired);

                var product = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.ProductsEndpoint)
                    .AppendPathSegment(productId)
                    .GetJsonAsync<Product>();

                response.Data = product;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<CandlesPage>> IProductsEndpoint.GetProductCandlesAsync(string productId, DateTime start, DateTime end, string granularity)
        {
            var response = new ApiResponse<CandlesPage>();

            try
            {
                //TODO Additional parameter validation
                if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentNullException(nameof(productId), ErrorMessages.ProductIdRequired);

                var candlesPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.ProductsEndpoint)
                    .AppendPathSegment(productId)
                    .AppendPathSegment(ApiEndpoints.CandlesEndpoint)
                    .SetQueryParam(RequestParameters.Start, start)
                    .SetQueryParam(RequestParameters.End, end)
                    .SetQueryParam(RequestParameters.Granularity, granularity)
                    .GetJsonAsync<CandlesPage>();

                response.Data = candlesPage;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<TradesPage>> IProductsEndpoint.GetMarketTradesAsync(string productId, int limit)
        {
            var response = new ApiResponse<TradesPage>();

            try
            {
                //TODO Additional parameter validation
                if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentNullException(nameof(productId), ErrorMessages.ProductIdRequired);

                var tradesPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.ProductsEndpoint)
                    .AppendPathSegment(productId)
                    .AppendPathSegment(ApiEndpoints.TickerEndpoint)
                    .SetQueryParam(RequestParameters.Limit, limit)
                    .GetJsonAsync<TradesPage>();

                response.Data = tradesPage;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }
    }
}
