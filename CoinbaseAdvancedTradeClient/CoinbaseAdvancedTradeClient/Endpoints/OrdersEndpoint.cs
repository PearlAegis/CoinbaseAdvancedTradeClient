using CoinbaseAdvancedTradeClient.Constants;
using CoinbaseAdvancedTradeClient.Interfaces.Endpoints;
using CoinbaseAdvancedTradeClient.Models.Api.Common;
using CoinbaseAdvancedTradeClient.Models.Api.Orders;
using CoinbaseAdvancedTradeClient.Models.Pages;
using Flurl.Http;

namespace CoinbaseAdvancedTradeClient
{
    public partial class CoinbaseAdvancedTradeApiClient : IOrdersEndpoint
    {
        public IOrdersEndpoint Orders => this;

        async Task<ApiResponse<FillsPage>> IOrdersEndpoint.GetListFills(string? orderId = null, string? productId = null, 
            DateTimeOffset? start = null, DateTimeOffset? end = null, int? limit = null, string? cursor = null)
        {
            var response = new ApiResponse<FillsPage>();

            try
            {
                //TODO Parameter Validation
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

        async Task<ApiResponse<OrdersPage>> IOrdersEndpoint.GetListOrders(string? productId = null, ICollection<string>? orderStatuses = null, int? limit = null,  
            DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, string? userNativeCurrency = null, string? orderType = null, string? orderSide = null, 
            string? cursor = null, string? productType = null, string? orderPlacementSource = null)
        {
            var response = new ApiResponse<OrdersPage>();

            try
            {
                //TODO Parameter Validation
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

        async Task<ApiResponse<Order>> IOrdersEndpoint.GetOrder(string orderId)
        {
            var response = new ApiResponse<Order>();

            try
            {
                //TODO Parameter Validation

                var order = await Config.ApiUrl
                    .WithClient(this)
                    .AppendPathSegment(ApiEndpoints.OrdersHistoricalEndpoint)
                    .AppendPathSegment(orderId)
                    .GetJsonAsync<Order>();

                response.Data = order;
                response.Success = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionResponseAsync(ex, response);
            }

            return response;
        }

        Task<object> IOrdersEndpoint.PostCancelOrders(string[] orderIds)
        {
            throw new NotImplementedException();
        }

        Task<object> IOrdersEndpoint.PostCreateOrder(object order)
        {
            throw new NotImplementedException();
        }
    }
}
