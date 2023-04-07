using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.Orders;
using CoinbaseAdvancedTradeClient.Models.Pages;
using CoinbaseAdvancedTradeClient.Resources;
using Flurl.Http;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IOrdersEndpoint
    {
        public IOrdersEndpoint Orders => this;

        async Task<ApiResponse<FillsPage>> IOrdersEndpoint.GetListFillsAsync(string? orderId = null, string? productId = null, 
            DateTimeOffset? start = null, DateTimeOffset? end = null, int? limit = null, string? cursor = null)
        {
            var response = new ApiResponse<FillsPage>();

            try
            {
                if (limit != null && limit != 0 && (limit < 1 || limit > 250)) throw new ArgumentException(ErrorMessages.LimitParameterRange, nameof(limit));

                if (start.Equals(DateTimeOffset.MinValue)) start = null;
                if (end.Equals(DateTimeOffset.MinValue)) end = null;

                var fillsPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersHistoricalFillsEndpoint)
                    .SetQueryParam(RequestParameters.OrderId, orderId)
                    .SetQueryParam(RequestParameters.ProductId, productId)
                    .SetQueryParam(RequestParameters.StartSequenceTimestamp, start)
                    .SetQueryParam(RequestParameters.EndSequenceTimestamp, end)
                    .SetQueryParam(RequestParameters.Limit, limit)
                    .SetQueryParam(RequestParameters.Cursor, cursor)
                    .GetJsonAsync<FillsPage>();

                response.Data = fillsPage;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<OrdersPage>> IOrdersEndpoint.GetListOrdersAsync(string? productId = null, ICollection<string>? orderStatuses = null, int? limit = null,  
            DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, string? userNativeCurrency = null, string? orderType = null, string? orderSide = null, 
            string? cursor = null, string? productType = null, string? orderPlacementSource = null)
        {
            var response = new ApiResponse<OrdersPage>();

            try
            {
                if (!string.IsNullOrWhiteSpace(productType) && !ProductTypes.ProductTypeList.Contains(productType)) throw new ArgumentException(ErrorMessages.ProductTypeInvalid, nameof(productType));
                if (!string.IsNullOrWhiteSpace(orderSide) && !OrderSides.OrderSideList.Contains(orderSide)) throw new ArgumentException(ErrorMessages.OrderSideInvalid, nameof(orderSide));
                if (!string.IsNullOrWhiteSpace(orderType) && !OrderTypes.OrderTypeList.Contains(orderType)) throw new ArgumentException(ErrorMessages.OrderTypeInvalid, nameof(orderType));
                if (!string.IsNullOrWhiteSpace(orderPlacementSource) && !OrderTypes.OrderTypeList.Contains(orderPlacementSource)) throw new ArgumentException(ErrorMessages.OrderPlacementSourceInvalid, nameof(orderPlacementSource));
                if (limit != null && limit != 0 && (limit < 1 || limit > 250)) throw new ArgumentException(ErrorMessages.LimitParameterRange, nameof(limit));

                if (orderStatuses != null && !orderStatuses.Any()) orderStatuses = null;
                if (startDate.Equals(DateTimeOffset.MinValue)) startDate = null;
                if (endDate.Equals(DateTimeOffset.MinValue)) endDate = null;

                var ordersPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersHistoricalBatchEndpoint)
                    .SetQueryParam(RequestParameters.ProductId, productId)
                    .SetQueryParam(RequestParameters.OrderStatus, orderStatuses?.ToArray())
                    .SetQueryParam(RequestParameters.Limit, limit)
                    .SetQueryParam(RequestParameters.StartDate, startDate)
                    .SetQueryParam(RequestParameters.EndDate, endDate)
                    .SetQueryParam(RequestParameters.UserNativeCurrency, userNativeCurrency)
                    .SetQueryParam(RequestParameters.OrderType, orderType)
                    .SetQueryParam(RequestParameters.OrderSide, orderSide)
                    .SetQueryParam(RequestParameters.Cursor, cursor)
                    .SetQueryParam(RequestParameters.ProductType, productType)
                    .SetQueryParam(RequestParameters.OrderPlacementSource, orderPlacementSource)
                    .GetJsonAsync<OrdersPage>();

                response.Data = ordersPage;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<Order>> IOrdersEndpoint.GetOrderAsync(string orderId)
        {
            var response = new ApiResponse<Order>();

            try
            {
                if (string.IsNullOrWhiteSpace(orderId)) throw new ArgumentNullException(nameof(orderId), ErrorMessages.OrderIdRequired);

                var ordersPage = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersHistoricalEndpoint)
                    .AppendPathSegment(orderId)
                    .GetJsonAsync<OrdersPage>();

                response.Data = ordersPage.Order;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<Order>> IOrdersEndpoint.PostCreateOrderAsync(CreateOrderParameters order, CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<Order>();

            try
            {
                var x = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersEndpoint)
                    .PostJsonAsync(order, cancellationToken)
                    .ReceiveJson();

                response.Data = x;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        async Task<ApiResponse<Order>> IOrdersEndpoint.PostCancelOrdersAsync(string[] orderIds, CancellationToken cancellationToken = default)
        {
            var response = new ApiResponse<Order>();

            try
            {
                var x = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersBatchCancelEndpoint)
                    .PostJsonAsync(orderIds, cancellationToken)
                    .ReceiveJson();

                response.Data = x;
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
