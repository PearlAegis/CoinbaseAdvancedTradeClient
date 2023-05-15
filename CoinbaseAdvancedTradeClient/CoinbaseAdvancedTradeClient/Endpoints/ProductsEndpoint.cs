using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Enums;
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

        async Task<ApiResponse<ProductsPage>> IProductsEndpoint.GetListProductsAsync(int? limit, int? offset, ProductType productType)
        {
            var response = new ApiResponse<ProductsPage>();

            try
            {
                if (limit != null && (limit < 1 || limit > 250)) throw new ArgumentException(ErrorMessages.LimitParameterRange, nameof(limit));
                if (offset != null && (offset < 0)) throw new ArgumentException(ErrorMessages.OffsetParameterRange, nameof(offset));

                var productsPage = await _config.ApiUrl
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

                var product = await _config.ApiUrl
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

        async Task<ApiResponse<CandlesPage>> IProductsEndpoint.GetProductCandlesAsync(string productId, DateTimeOffset start, DateTimeOffset end, string granularity)
        {
            var response = new ApiResponse<CandlesPage>();

            try
            {
                if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentNullException(nameof(productId), ErrorMessages.ProductIdRequired);
                if (start.Equals(DateTimeOffset.MinValue)) throw new ArgumentException(ErrorMessages.StartDateRequired, nameof(start));
                if (end.Equals(DateTimeOffset.MinValue)) throw new ArgumentException(ErrorMessages.EndDateRequired, nameof(end));
                if (!CandleGranularity.CandleGranularityList.Contains(granularity, StringComparer.InvariantCultureIgnoreCase)) throw new ArgumentException(ErrorMessages.CandleGranularityInvalid, nameof(granularity));

                var candlesPage = await _config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.ProductsEndpoint)
                    .AppendPathSegment(productId)
                    .AppendPathSegment(ApiEndpoints.CandlesEndpoint)
                    .SetQueryParam(RequestParameters.Start, start.ToUnixTimeSeconds())
                    .SetQueryParam(RequestParameters.End, end.ToUnixTimeSeconds())
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
                if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentNullException(nameof(productId), ErrorMessages.ProductIdRequired);
                if (limit < 1 || limit > 250) throw new ArgumentException(ErrorMessages.LimitParameterRange, nameof(limit));

                var tradesPage = await _config.ApiUrl
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
